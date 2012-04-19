using System.Collections.Generic;

namespace MediaPickerService.Dto
{
    public class Transport
    {
        public IEnumerable<Breadcrumb> Breadcrumbs { get; set; }
        public IEnumerable<Media> Mediae { get; set; }
    }
}
