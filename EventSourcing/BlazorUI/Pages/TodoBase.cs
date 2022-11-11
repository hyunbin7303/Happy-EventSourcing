﻿using HP.Application.Commands;
using HP.Application.DTOs;
using HP.Application.Queries.Todos;
using HP.Domain;
using HP.GeneralUI.DropdownControl;
using HP.Shared;
using HP.Shared.Contacts;
using MediatR;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace BlazorUI.Pages
{
    public class TodoBase : ComponentBase
    {
        [Inject] public IMediator Mediator { get; set; }
        [Inject] public NavigationManager NavigationManager { get; set; }
        [Inject] private ICurrentUserService CurrentUserService { get; set; }
        [Parameter] public string TodoId { get; set; }
        public TodoDetailsDto TodoDetails { get; private set; } = new();
        public TodoDetailsDto TodoDetailsFromTodoSearch { get; private set; }
        public IEnumerable<TodoDetailsDto> Todos { get; set; } = new List<TodoDetailsDto>();
        private string _deleteTodoId { get; set; } = string.Empty;
        protected EditContext EditContext { get; set; }
        protected CreateTodoModel CreateTodoModel { get; set; } = new();
        protected IList<DropdownItem<TodoType>> TodoTypeEnums { get; } = new List<DropdownItem<TodoType>>();
        protected DropdownItem<TodoType> SelectedTodoTypeDropDownItem { get; set; }
        protected IList<DropdownItem<TodoStatus>> TodoStatusEnums { get; } = new List<DropdownItem<TodoStatus>>();
        protected DropdownItem<TodoStatus> SelectedTodoStatusDropDownItem { get; set; }
        public bool DeleteDialogOpen { get; set; }
        public string TodoIdInput { get; set; }
        public TodoBase()
        {
            foreach (TodoType type in TodoType.List())
            {
                var ddItem = new DropdownItem<TodoType>
                {
                    ItemObject = type,
                    DisplayText = type.Name
                };
                TodoTypeEnums.Add(ddItem);
            }

            foreach (TodoStatus type in TodoStatus.List())
            {
                var ddStatus = new DropdownItem<TodoStatus>
                {
                    ItemObject = type,
                    DisplayText = type.Name
                };
                TodoStatusEnums.Add(ddStatus);
            }
            SelectedTodoTypeDropDownItem = TodoTypeEnums[0];
            SelectedTodoStatusDropDownItem = TodoStatusEnums[0];
        }
        protected override async Task OnInitializedAsync()
        {
            EditContext = new EditContext(CreateTodoModel);
            await LoadData();
        }
        public async void SearchChanged(string value)
        {
            if (string.IsNullOrEmpty(value))
                return;
            var getTodo = await Mediator.Send(new GetTodoById(value));
            if (getTodo == null)
            {
                TodoDetailsFromTodoSearch = null;
                return;
            }
            TodoDetailsFromTodoSearch = getTodo;
        }
        private async Task LoadData()
        {
            var temp_username = CurrentUserService.CurrentUser.UserName;
            Todos = await Mediator.Send(new GetTodosByUserId(temp_username));
            StateHasChanged();
        }
        protected async void OnSubmit()
        {
            TodoType todoType = SelectedTodoTypeDropDownItem.ItemObject;
            TodoDetailsDto newTodo = await Mediator.Send(new CreateTodoCommand(CreateTodoModel.UserId, CreateTodoModel.Title, todoType.Name, CreateTodoModel.Description));
            NavigationManager.NavigateTo("todos");
        }
        protected void OnClickViewDetails(string todoId)
        {
            NavigationManager.NavigateTo($"todos/details/{todoId}");
        }
        protected void OnClickGoToCreateTodo()
        {
            NavigationManager.NavigateTo("todos/create");
        }
        protected IOrderedEnumerable<IGrouping<string, TodoDetailsDto>> GetTodosByUserName()
        {
            return null;
        }
        protected async Task OnDeleteDialogClose(bool accepted)
        {
            if (accepted)
            {
                await Mediator.Send(new DeleteTodoCommand(_deleteTodoId));
                _deleteTodoId = string.Empty;
            }
            DeleteDialogOpen = false;
            StateHasChanged();
        }
        protected async void OpenDeleteDialog(string todoId)
        {
            DeleteDialogOpen = true;
            _deleteTodoId = todoId;
            await LoadData();
        }
    }
}
