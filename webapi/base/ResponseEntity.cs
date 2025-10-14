using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

public class ResponseEntity : IActionResult
{
    public int StatusCode { get; set; }
    // truỳ nội dung trả về 
    public object Content { get; set; }
    public string Message { get; set; }
    public DateTime DateTime { get; set; } = DateTime.Now;

    public ResponseEntity(int statusCode, object content, string message = "")
    {
        StatusCode = statusCode;
        Content = content;
        Message = message;
    }
    public async Task ExecuteResultAsync(ActionContext context)
    {
        var response = context.HttpContext.Response;
        response.StatusCode = StatusCode;
        response.ContentType = "application/json";

        var payload = new
        {
            statusCode = StatusCode,
            message = Message,
            content = Content,
            dateTime = DateTime
        };

        // Serialize ra JSON (đảm bảo dùng UTF-8, ignore nulls)
        var json = JsonSerializer.Serialize(payload, new JsonSerializerOptions
        {
            WriteIndented = true,
            DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
        });

        await response.WriteAsync(json);
    }

}