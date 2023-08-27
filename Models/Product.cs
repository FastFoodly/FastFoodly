using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;

namespace FastFoodly.Models
{
    /// <summary>
    /// A classe Produto guarda as informações sobre o produto do cardápio. Herda ObservableObject
    /// A classe ObservableObject, fornecida pelo Community Toolkit MVVM, permite a notificação de alterações de propriedades.
    /// </summary>
    public class Product : ObservableObject
    {
        private int? productId; ///< Atributo que guarda o Id do produto do item do carrinho
        private string? name; ///< Atributo que guarda o nome do item do carrinho
        private decimal? price; ///< Atributo que guarda o preço do item do carrinho
        private string? description; ///< Atributo que guarda a descrição do produto
        private ObservableCollection<string>? extras; ///< Atributo que guarda uma lista de extras que podem ser customizados
        private string? category; ///< Atributo que guarda a categoria que o produto pertence
        private Uri? imagePath; ///< Atributo que guarda o caminho para acessar a imagem do produto

        /// <summary>
        /// Propriedade productId, que representa o id do produto.
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
        /// Propriedade price, que representa o preço do produto.
        /// </summary>
        public decimal? Price
        {
            get { return price; }
            //método SetProperty para notificar os assinantes sobre a alteração e atualizar o valor da propriedade.
            set { SetProperty(ref price, value); }
        }

        /// <summary>
        /// Propriedade Description, que representa o descrição do produto.
        /// </summary>
        public string? Description
        {
            get { return description; }
            //método SetProperty para notificar os assinantes sobre a alteração e atualizar o valor da propriedade.
            set { SetProperty(ref description, value); }
        }

        /// <summary>
        /// Propriedade Extras, que representa os ingredientes do produto.
        /// </summary>
        public ObservableCollection<string>? Extras
        {
            get { return extras; }
            //método SetProperty para notificar os assinantes sobre a alteração e atualizar o valor da propriedade.
            set { SetProperty(ref extras, value); }
        }

        /// <summary>
        /// Propriedade Category, que representa a categoria do produto.
        /// </summary>
        public string? Category
        {
            get { return category; }
            //método SetProperty para notificar os assinantes sobre a alteração e atualizar o valor da propriedade.
            set { SetProperty(ref category, value); }
        }

        /// <summary>
        /// Propriedade ImagePath, que representa o caminho da imagem do produto.
        /// </summary>
        public Uri? ImagePath
        {
            get { return imagePath; }
            //método SetProperty para notificar os assinantes sobre a alteração e atualizar o valor da propriedade.
            set { SetProperty(ref imagePath, value); }
        }

    }
}