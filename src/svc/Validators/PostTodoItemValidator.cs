using FluentValidation;
using TodoApi.Shared.DTOs;

public class PostTodoItemValidator : AbstractValidator<PostTodoItem>
{
    public PostTodoItemValidator()
    {
        //If the model passed in is null, there was an issue with the model binding.
        RuleFor(x => x).NotNull().WithMessage("There was an issue with the model data");
        RuleFor(x => x.Name).NotNull().NotEmpty().WithMessage("The name for a task cannot be null or empty");
    }
}