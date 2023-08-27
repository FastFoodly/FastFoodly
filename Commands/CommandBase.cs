using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace FastFoodly.Commands
{
    /// <summary>
    /// Classe abstrata base que permite criar classes que executam certos comandos
    /// </summary>
    public abstract class CommandBase : ICommand
    {
        public event EventHandler CanExecuteChanged; ///< Evento para quando a permissão do método Execute() mudar

        /// <summary>
        /// Método que define se o Execute() pode ou não ser ativado
        /// Retorno padrão como True
        /// Pode ser sobrescrito pelas classes que herdam essa
        /// </summary>
        /// <param name="parameter">Objeto genérico que poderia ser enviado como parâmetro</param>
        public virtual bool CanExecute(object parameter) => true;
        
        /// <summary>
        /// Método abstrato que deve possuir lógica executa pelo comando que herda essa classe
        /// </summary>
        /// <param name="parameter">Objeto genérico que poderia ser enviado como parâmetro</param>
        public abstract void Execute(object parameter);

        /// <summary>
        /// Função que é chamada sempre que a permissão de execução mudar
        /// </summary>
        protected void OnCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, new EventArgs());
        }
    }
}
