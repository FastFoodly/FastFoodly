using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;

namespace FastFoodly.Models
{
    /// <summary>
    /// A classe Extra guarda as informações sobre um complemento de um produto do cardápio. Herda ObservableObject
    /// A classe ObservableObject, fornecida pelo Community Toolkit MVVM, permite a notificação de alterações de propriedades.
    /// </summary>
    public class Extra : ObservableObject
    {
        private int? quantity; ///< Atributo que guarda a quantidade do complemento
        private string? name; ///< Atributo que guarda o nome do complemento

        /// <summary>
        /// Propriedade Quantity, que representa a quantidade desse extra
        /// </summary>
        public int? Quantity
        {
            get { return quantity; }
            //método SetProperty para notificar os assinantes sobre a alteração e atualizar o valor da propriedade.
            set { SetProperty(ref quantity, value); }
        }

        /// <summary>
        /// Propriedade Name, que representa o nome do complemento.
        /// </summary>
        public string? Name
        {
            get { return name; }
            //método SetProperty para notificar os assinantes sobre a alteração e atualizar o valor da propriedade.
            set { SetProperty(ref name, value); }
        }
    }
}