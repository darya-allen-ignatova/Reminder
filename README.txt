Для того, чтобы развернуть веб-приложение "Напоминалка" необходимо скачать файлы из репозитория. В папке Database вы найдете скрипт для создания базы данных, а также .bak файл, с помощью которого также можно восстановить данную базу данных. 
При использовании скрипта нужно поменять строчки :
"ON  PRIMARY  ( NAME = N'DI.Reminder.DataBase', FILENAME = N'C:\Program Files (x86)\Microsoft SQL Server\MSSQL11.SQLEXPRESS\MSSQL\DATA\DI.Reminder.DataBase.mdf' , SIZE = 5120KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'DI.Reminder.DataBase_log', FILENAME = N'C:\Program Files (x86)\Microsoft SQL Server\MSSQL11.SQLEXPRESS\MSSQL\DATA\DI.Reminder.DataBase_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO"
Вместо FILENAME нужно указать пути для базы данных и лога. 
Останется поменять строки подключения к БД:
/Application/DI.Reminder.Web/Web.config
/Service/DI.Reminder.Service/Web.config
Данное приложение теперь готово к использованию. Зайти можно под профилем "Admin" и паролем "AdminAdmin".