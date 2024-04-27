namespace Frank.Blazor.JsInteropGenerator.Internals.Js;

public class JsTypeToCsharpTypeConverter
{
    public string Convert(JsType jsType)
    {
        return jsType switch
        {
            JsType.Number => "double",
            JsType.String => "string",
            JsType.Boolean => "bool",
            JsType.Object => "object",
            JsType.Undefined => "void",
            JsType.Null => "object",
            _ => throw new ArgumentOutOfRangeException(nameof(jsType), jsType, null)
        };
    }
}