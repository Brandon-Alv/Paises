using Paises.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Paises.ViewModels
{
    public class PaisViewModel :BaseViewModel
    {
        #region Atributos
        
        #endregion

        #region Propiedades
        public Pais PaisSelect
        {
            get;
            set;
        }
        #endregion

        #region Constructor
        public PaisViewModel(Pais pais)
        {

            this.PaisSelect = pais;
        }
        #endregion
    }
}
