using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;

namespace FastFoodly.Models
{
    /// <summary>
    /// A classe Order guarda as informações sobre um pedido. Herda ObservableObject
    /// A classe ObservableObject, fornecida pelo Community Toolkit MVVM, permite a notificação de alterações de propriedades.
    /// </summary>
    public class Order : ObservableObject
    {
        private string? productIds; ///< Atributo que armazena os Ids dos produtos comprados
        private decimal? totalPrice; ///< Atributo que armazena o preço total do pedido
        private string? observations; ///< Atributo que armazena as observações do pedido

        /// <summary>
        /// Propriedade ProductIds, que representa os ids dos produtos.
        /// </summary>
        public string? ProductIds
        {
            get { return productIds; }
            //método SetProperty para notificar os assinantes sobre a alteração e atualizar o valor da propriedade.
            set { SetProperty(ref productIds, value); }
        }

        /// <summary>
        /// Propriedade TotalPrice, que representa o preço total do pedido.
        /// </summary>
        public decimal? TotalPrice
        {
            get { return totalPrice; }
            //método SetProperty para notificar os assinantes sobre a alteração e atualizar o valor da propriedade.
            set { SetProperty(ref totalPrice, value); }
        }

        /// <summary>
        /// Propriedade Observations, que representa observações sobre o pedido e seus adicionais.
        /// </summary>
        public string? Observations
        {
            get { return observations; }
            //método SetProperty para notificar os assinantes sobre a alteração e atualizar o valor da propriedade.
            set { SetProperty(ref observations, value); }
        }
    }
}