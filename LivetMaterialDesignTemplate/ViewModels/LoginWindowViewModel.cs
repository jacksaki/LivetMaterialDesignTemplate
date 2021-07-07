using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections.ObjectModel;
using Livet;
using Livet.Commands;
using Livet.Messaging;
using Livet.Messaging.IO;
using Livet.EventListeners;
using Livet.Messaging.Windows;

using LivetMaterialDesignTemplate.Models;
using System.Windows;

namespace LivetMaterialDesignTemplate.ViewModels {
    public class LoginWindowViewModel : ViewModel {
        /* コマンド、プロパティの定義にはそれぞれ
         *
         *  lvcom   : ViewModelCommand
         *  lvcomn  : ViewModelCommand(CanExecute無)
         *  llcom   : ListenerCommand(パラメータ有のコマンド)
         *  llcomn  : ListenerCommand(パラメータ有のコマンド・CanExecute無)
         *  lprop   : 変更通知プロパティ(.NET4.5ではlpropn)
         *
         * を使用してください。
         *
         * Modelが十分にリッチであるならコマンドにこだわる必要はありません。
         * View側のコードビハインドを使用しないMVVMパターンの実装を行う場合でも、ViewModelにメソッドを定義し、
         * LivetCallMethodActionなどから直接メソッドを呼び出してください。
         *
         * ViewModelのコマンドを呼び出せるLivetのすべてのビヘイビア・トリガー・アクションは
         * 同様に直接ViewModelのメソッドを呼び出し可能です。
         */

        /* ViewModelからViewを操作したい場合は、View側のコードビハインド無で処理を行いたい場合は
         * Messengerプロパティからメッセージ(各種InteractionMessage)を発信する事を検討してください。
         */

        /* Modelからの変更通知などの各種イベントを受け取る場合は、PropertyChangedEventListenerや
         * CollectionChangedEventListenerを使うと便利です。各種ListenerはViewModelに定義されている
         * CompositeDisposableプロパティ(LivetCompositeDisposable型)に格納しておく事でイベント解放を容易に行えます。
         *
         * ReactiveExtensionsなどを併用する場合は、ReactiveExtensionsのCompositeDisposableを
         * ViewModelのCompositeDisposableプロパティに格納しておくのを推奨します。
         *
         * LivetのWindowテンプレートではViewのウィンドウが閉じる際にDataContextDisposeActionが動作するようになっており、
         * ViewModelのDisposeが呼ばれCompositeDisposableプロパティに格納されたすべてのIDisposable型のインスタンスが解放されます。
         *
         * ViewModelを使いまわしたい時などは、ViewからDataContextDisposeActionを取り除くか、発動のタイミングをずらす事で対応可能です。
         */

        /* UIDispatcherを操作する場合は、DispatcherHelperのメソッドを操作してください。
         * UIDispatcher自体はApp.xaml.csでインスタンスを確保してあります。
         *
         * LivetのViewModelではプロパティ変更通知(RaisePropertyChanged)やDispatcherCollectionを使ったコレクション変更通知は
         * 自動的にUIDispatcher上での通知に変換されます。変更通知に際してUIDispatcherを操作する必要はありません。
         */

        public void Initialize() {
        }
        public LoginWindowViewModel() : base() {
            this.MahAppsDialogCoordinator = MahApps.Metro.Controls.Dialogs.DialogCoordinator.Instance;
            this.LoginParameter = new LoginParameter();
            this.LoginParameter.PropertyChanged += (sender, e) => {
                LoginCommand.RaiseCanExecuteChanged();
            };
        }

        public MahApps.Metro.Controls.Dialogs.IDialogCoordinator MahAppsDialogCoordinator {
            get;
            set;
        }

        private ViewModelCommand _CancelCommand;

        public ViewModelCommand CancelCommand {
            get {
                if (_CancelCommand == null) {
                    _CancelCommand = new ViewModelCommand(Cancel);
                }
                return _CancelCommand;
            }
        }

        public void Cancel() {
            this.DialogResult = false;
        }

        #region LoginCommand
        private ViewModelCommand _LoginCommand;

        public ViewModelCommand LoginCommand {
            get {
                if (_LoginCommand == null) {
                    _LoginCommand = new ViewModelCommand(Login, CanLogin);
                }
                return _LoginCommand;
            }
        }
        public bool CanLogin() {
            try {
                this.LoginParameter.Validate();
                return true;
            } catch {
                return false;
            }
        }
        public LoginParameter LoginParameter {
            get;
        }
        public void Login() {
            try {
                LoginUser.GetInstance().Login(this.LoginParameter);
                this.DialogResult = true;
            } catch (Exception ex) {
                this.MahAppsDialogCoordinator.ShowMessageAsync(this, "エラー", ex.Message, MahApps.Metro.Controls.Dialogs.MessageDialogStyle.Affirmative);
            }
        }
        #endregion
        public void ShowErrorDialog(string message, string title) {
            Messenger.Raise(new InformationMessage(message, title, MessageBoxImage.Error, "Error"));
        }


        #region DialogResult変更通知プロパティ
        private bool? _DialogResult;

        public bool? DialogResult {
            get {
                return _DialogResult;
            }
            private set {
                if (_DialogResult == value) {
                    return;
                }
                _DialogResult = value;
                RaisePropertyChanged(nameof(DialogResult));
            }
        }
        #endregion

    }
}
