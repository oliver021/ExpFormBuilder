using MudFormBuilder.Attributes;

public class PasswordAttribute : FormAttribute
{
    public bool HasConfirmation { get; set; }
    public PasswordAttribute(bool hasConfirm = false)
    {
        HasConfirmation = hasConfirm;
    }
}
