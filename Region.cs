﻿namespace RegionsAPI.Data
{
    public class Region
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public Country Country { get; set; }
    }
}
