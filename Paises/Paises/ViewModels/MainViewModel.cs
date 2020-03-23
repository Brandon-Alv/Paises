using Paises.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Paises.ViewModels
{
    public class MainViewModel
    {
        #region ViewModels
        public PaisesViewModel Paises { get; set; }
        public PaisViewModel Pais { get; set; }
        #endregion

        #region Propiedades
        public List<Pais> ListaPaises { get; set; }
        #endregion

        #region Constructor
        public MainViewModel()
        {
            instance = this;
            this.Paises = new PaisesViewModel();
        }
        #endregion

        #region Singleton
        private static MainViewModel instance;

        public static MainViewModel GetInstance()
        {
            if(instance == null)
            {
                return new MainViewModel();
            }

            return instance;
        }
        #endregion
    }
}
