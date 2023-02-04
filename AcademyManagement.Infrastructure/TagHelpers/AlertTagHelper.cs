using Microsoft.AspNetCore.Razor.TagHelpers;

namespace AcademyManagement.Infrastructure.TagHelpers
{
    [HtmlTargetElement("Alert")]
    public class AlertTagHelper : TagHelper
    {
        public string Message { get; set; }
        public enum AlertType
        {
            success,
            primary,
            danger,
            info,
            warning
        }

        public AlertType Type { get; set; }

        public string Classes { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            output.TagMode = TagMode.StartTagAndEndTag;
            output.Attributes.SetAttribute("class", $"{Classes} alert alert-" + Type.ToString());
            var pTag = output.PreContent.SetHtmlContent("<p class=text-center>");
            output.PostContent.SetHtmlContent("</p>");
    
            output.Content.SetContent(Message);

        }
    }
}
