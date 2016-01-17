using System;
using System.ServiceModel;

namespace Cush.Windows.Services
{
    public interface IHostEventResponder
    {
        /// <summary>
        ///     This method should be called when
        ///     the <see cref="System.ServiceModel.ServiceHost" /> transitions into the closed state.
        /// </summary>
        void OnClosed(object sender, EventArgs e);

        /// <summary>
        ///     This method should be called when
        ///     the <see cref="System.ServiceModel.ServiceHost" /> transitions into the closing state.
        /// </summary>
        void OnClosing(object sender, EventArgs e);

        /// <summary>
        ///     This method should be called when
        ///     the <see cref="System.ServiceModel.ServiceHost" /> transitions into the opening state.
        /// </summary>
        void OnOpening(object sender, EventArgs e);

        /// <summary>
        ///     This method should be called when
        ///     the <see cref="System.ServiceModel.ServiceHost" /> transitions into the opened state.
        /// </summary>
        void OnOpened(object sender, EventArgs e);

        /// <summary>
        ///     This method should be called when
        ///     the <see cref="System.ServiceModel.ServiceHost" /> transitions into the faulted state.
        /// </summary>
        void OnFaulted(object sender, EventArgs e);

        /// <summary>
        ///     This method should be called when
        ///     the <see cref="System.ServiceModel.ServiceHost.UnknownMessageReceived " /> event is fired.
        /// </summary>
        void OnUnknownMessageReceived(object sender, UnknownMessageReceivedEventArgs e);
    }
}