using System;
using System.Collections.Generic;
using System.Windows;

namespace Lab3
{
    static class WindowManager
    {
        static readonly Dictionary<Tuple<Type, string>, Type> _window = new Dictionary<Tuple<Type, string>, Type>();

        public static void Register(Type objType, string action, Type viewType)
        {
            _window[new Tuple<Type, string>(objType, action)] = viewType;
        }

        public static bool ShowDialog(object viewModel, string action)
        {
            var viewType = _window[new Tuple<Type, string>(viewModel.GetType(), action)];
            var wnd = Activator.CreateInstance(viewType) as Window;
            wnd.DataContext = viewModel;
            return wnd.ShowDialog() is true;
        }
    }
}
