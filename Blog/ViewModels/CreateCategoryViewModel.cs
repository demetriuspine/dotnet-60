﻿namespace Blog.ViewModels
{
    public class CreateCategoryViewModel // apenas name e slug serão mandados plo postman/frontend
    {
        public string Name { get; set; }
        public string Slug { get; set; }
    }
}
