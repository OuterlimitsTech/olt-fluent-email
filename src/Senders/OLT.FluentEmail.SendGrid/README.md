![fluent email logo](https://raw.githubusercontent.com/lukencode/FluentEmail/master/assets/fluentemail_logo_64x64.png "FluentEmail")
# This package add provides an Advanced SendGrid Sender for FluentEmail 

See [FluentEmail (jcamp version)](https://github.com/jcamp-code/FluentEmail) for more information


**Send SendGrid Template via the SendGrid API with additional options**

- DisableClickTracking
- DisableOpenTracking
- CustomArgs
- UnsubscribeGroupId


```csharp
public class EmailService {

   private IFluentEmail _fluentEmail;

   public EmailService(IFluentEmail fluentEmail) {
     _fluentEmail = fluentEmail;
   }

   public async Task Send() {
    var myTemplateJson = new MyTemplateClass();

    var result = await _fluentEmail
        .To("somebody@gmail.com")

        // NOTE: You do not have to provide template data, you can pass null
        .SendWithTemplateAsync("d-templateIdHere", myTemplateJson, opts =>
        {
            opts.DisableClickTracking = true;
            opts.DisableOpenTracking = true;
            opts.CustomArgs.Add("CustomArgName", "ArgValue");
            opts.UnsubscribeGroupId = 123456;
        });
   }
}
```