using System;
using System.Collections.Generic;
using CommunityToolkit.Mvvm.ComponentModel;

namespace FastFoodly.Models
{
    /// <summary>
    /// A classe CartItem guarda as informações do carrinho de compras que herda ObservableObject
    /// A classe ObservableObject, fornecida pelo Community Toolkit MVVM, permite a notificação de alterações de propriedades.
    /// </summary>
    public class CartItem : ObservableObject
    {
        private int itemId; ///< Atributo que guarda o Id do item do carrinho
        private int? productId; ///< Atributo que guarda o Id do produto do item do carrinho
        private string? name; ///< Atributo que guarda o nome do item do carrinho
        private decimal? price; ///< Atributo que guarda o preço do item do carrinho
        private int? quantity; ///< Atributo que guarda a quantidade do item
        private string? observations; ///< Atributo que guarda observações sobre o item
        private Uri? imagePath; ///< Atributo que guarda o caminho para acessar a imagem do produto
        
        /// <summary>
        /// Propriedade ItemId, que representa o id do item do carrinho.
        /// </summary>
        public int ItemId
        {
            get { return itemId; }
            //método SetProperty para notificar os assinantes sobre a alteração e atualizar o valor da propriedade.
            set { SetProperty(ref itemId, value); }
        }

        /// <summary>
        /// Propriedade ProductId, que representa o id do produto.
        /// </summary>
        public int? ProductId
        {
            get { return productId; }
            //método SetProperty para notificar os assinantes sobre a alteração e atualizar o valor da propriedade.
            set { SetProperty(ref productId, value); }
        }

        /// <summary>
        /// Propriedade Name, que representa o nome do produto.
        /// </summary>
        public string? Name
        {
            get { return name; }
            //método SetProperty para notificar os assinantes sobre a alteração e atualizar o valor da propriedade.
            set { SetProperty(ref name, value); }
        }

        /// <summary>
        /// Propriedade Price, que representa o preço do produto.
        /// </summary>
        public decimal? Price
        {
            get { return price; }
            //método SetProperty para notificar os assinantes sobre a alteração e atualizar o valor da propriedade.
            set { SetProperty(ref price, value); }
        }

        /// <summary>
        /// Propriedade Quantity, que representa a quantidade de itens desse produto.
        /// </summary>
        public int? Quantity
        {
            get { return quantity; }
            //método SetProperty para notificar os assinantes sobre a alteração e atualizar o valor da propriedade.
            set { SetProperty(ref quantity, value); }
        }

        /// <summary>
        /// Propriedade Observations, que representa observações sobre o item do carrinho.
        /// </summary>
        public string? Observations
        {
            get { return observations; }
            //método SetProperty para notificar os assinantes sobre a alteração e atualizar o valor da propriedade.
            set { SetProperty(ref observations, value); }
        }

        /// <summary>
        /// Propriedade ImagePath, que representa o caminho para a imagem do produto
        /// </summary>
        public Uri? ImagePath
        {
            get { return imagePath; }
            //método SetProperty para notificar os assinantes sobre a alteração e atualizar o valor da propriedade.
            set { SetProperty(ref imagePath, value); }
        }

    }
}