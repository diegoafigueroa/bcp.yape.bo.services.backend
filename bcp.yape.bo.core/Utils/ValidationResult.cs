/// <summary>
/// Represents the result of a validation operation.
/// </summary>
public class ValidationResult
{
    /// <summary>
    /// Gets or sets a value indicating whether the validation was successful.
    /// </summary>
    /// <value>
    /// <c>true</c> if the validation was successful; otherwise, <c>false</c>.
    /// </value>
    public bool Success { get; set; }

    /// <summary>
    /// Gets or sets the message that provides additional information about the validation result.
    /// </summary>
    /// <value>
    /// A string containing a message that explains the outcome of the validation. 
    /// This can be an error message or a success message.
    /// </value>
    public string Message { get; set; }

    /// <summary>
    /// Gets or sets the object that is returned if validation is successful.
    /// This object is optional and should only be set if validation is successful.
    /// </summary>
    public object ReturnObject { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="ValidationResult"/> class with the specified success status, optional message, and optional return object.
    /// </summary>
    /// <param name="success">A boolean indicating whether the validation was successful (<c>true</c>) or failed (<c>false</c>).</param>
    /// <param name="message">An optional message that provides additional information about the validation result (default is an empty string).</param>
    /// <param name="returnObject">An optional object that will be returned if validation is successful.</param>
    public ValidationResult(bool success, string message = "", object returnObject = null)
    {
        Success = success;
        Message = message;
        ReturnObject = returnObject;
    }
}
