using Paises.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Paises.Infrastructure
{
    class InstanceLocator
    {
        public MainViewModel Main { get; set; }

        public InstanceLocator()
        {
            this.Main = new MainViewModel();
        }
    }
}
