﻿using HP.Api.Requests;
using HP.Application.DTOs;
using HP.Application.Handlers;
using HP.Application.Queries.Todos;
using HP.Domain;
using HP.GeneralUI.DropdownControl;
using HP.Shared;
using MediatR;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace BlazorUI.Pages
{
    public class TodoBase : ComponentBase
    {
        [Inject]
        public IMediator Mediator { get; set; }
        public TodoDetailsDto TodoDetails { get; private set; }
        public string Result { get; set; }
        public IEnumerable<TodoBasicInfoDto> Todos { get; set; }
        protected EditContext EditContext { get; set; }
        protected CreateTodoModel CreateTodoModel { get; set; } = new();
        protected IList<DropdownItem<TodoType>> TodoTypeEnums { get; } = new List<DropdownItem<TodoType>>();
        protected DropdownItem<TodoType> SelectedTodoTypeDropDownItem { get; set; }

        public TodoBase()
        {

            var todoInfo = new DropdownItem<TodoType>
            {
                ItemObject = TodoType.Work,
                DisplayText = "Work"
            };
            var todoInfo2 = new DropdownItem<TodoType>
            {
                ItemObject = TodoType.Research,
                DisplayText = "Research"
            };
            var todoInfo3 = new DropdownItem<TodoType>
            {
                ItemObject = TodoType.Others,
                DisplayText = "Others"
            };
            var todoInfo4 = new DropdownItem<TodoType>
            {
                ItemObject = TodoType.Chores,
                DisplayText = "Chores"
            };
            TodoTypeEnums.Add(todoInfo);
            TodoTypeEnums.Add(todoInfo2);
            TodoTypeEnums.Add(todoInfo3);
            TodoTypeEnums.Add(todoInfo4);
            SelectedTodoTypeDropDownItem = todoInfo3;
        }
        protected override async Task OnInitializedAsync()
        {
            base.OnInitialized();
            Todos = new List<TodoBasicInfoDto>();
            EditContext = new EditContext(CreateTodoModel);
            Todos = await Mediator.Send(new GetTodos());
        }
        protected async void OnSubmit()
        {
            TodoType todoType =SelectedTodoTypeDropDownItem.ItemObject;
            TodoDetailsDto newTodo = await Mediator.Send(new CreateTodoCommand(CreateTodoModel.UserId, CreateTodoModel.Title, todoType.Name,CreateTodoModel.Description));
        }
    }
}
