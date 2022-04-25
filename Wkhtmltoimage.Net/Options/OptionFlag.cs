using System;

namespace Wkhtmltoimage.Net.Options
{
    public class OptionFlag : Attribute
    {
        public string Name { get; private set; }

        public OptionFlag(string name)
        {
            Name = name;
        }
    }
}