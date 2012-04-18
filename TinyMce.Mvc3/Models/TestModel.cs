using System.ComponentModel.DataAnnotations;

namespace TinyMce.Mvc3.Models
{
    public class TestModel
    {
        [UIHint("TinyMce")]
        public string Content { get; set; }
    }
}