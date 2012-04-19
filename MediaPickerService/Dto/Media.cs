﻿namespace MediaPickerService.Dto
{
    public class Media
    {
        public bool IsDirectory { get; set; }
        public string Url { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }
        public double? Size { get; set; }
    }
}