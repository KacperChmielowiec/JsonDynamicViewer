using Avalonia.Controls;
using MessageBox.Avalonia.BaseWindows.Base;
using MessageBox.Avalonia.DTO;
using MessageBox.Avalonia.Enums;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avalonia3.Services
{
    public static class MessageService
    {
        public static IMsBoxWindow<ButtonResult> TabDialog()
        {
            return MessageBox.Avalonia.MessageBoxManager.GetMessageBoxStandardWindow(
                new MessageBoxStandardParams
                {
                    ButtonDefinitions = ButtonEnum.YesNo,
                    ContentTitle = "Message",
                    ContentHeader = "Plik Json",
                    ContentMessage = "Czy chcesz otowrzyć nowe okno ",
                    WindowIcon = new WindowIcon("C:\\Users\\kacper\\source\\repos\\Avalonia3\\Avalonia3\\images\\question_mark.png"),
                    Icon = MessageBox.Avalonia.Enums.Icon.Question,
                    WindowStartupLocation = WindowStartupLocation.CenterOwner
                });
        }
        public static IMsBoxWindow<ButtonResult> ExceptionLoadDialog(string Info)
        {
            return MessageBox.Avalonia.MessageBoxManager.GetMessageBoxStandardWindow(
                new MessageBoxStandardParams
                {
                    ButtonDefinitions = ButtonEnum.Ok,
                    ContentTitle = "Message",
                    ContentHeader = "Occured Loading Exception",
                    ContentMessage = $"{Info}",
                    WindowIcon = new WindowIcon("C:\\Users\\kacper\\source\\repos\\Avalonia3\\Avalonia3\\images\\question_mark.png"),
                    Icon = MessageBox.Avalonia.Enums.Icon.Error,
                    WindowStartupLocation = WindowStartupLocation.CenterScreen,
                    ShowInCenter = true
                   
                }); ;
        }
    }
}
