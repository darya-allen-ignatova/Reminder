��� ����, ����� ���������� ���-���������� "�����������" ���������� ������� ����� �� �����������. � ����� Database �� ������� ������ ��� �������� ���� ������, � ����� .bak ����, � ������� �������� ����� ����� ������������ ������ ���� ������. 
��� ������������� ������� ����� �������� ������� :
"ON  PRIMARY  ( NAME = N'DI.Reminder.DataBase', FILENAME = N'C:\Program Files (x86)\Microsoft SQL Server\MSSQL11.SQLEXPRESS\MSSQL\DATA\DI.Reminder.DataBase.mdf' , SIZE = 5120KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'DI.Reminder.DataBase_log', FILENAME = N'C:\Program Files (x86)\Microsoft SQL Server\MSSQL11.SQLEXPRESS\MSSQL\DATA\DI.Reminder.DataBase_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO"
������ FILENAME ����� ������� ���� ��� ���� ������ � ����. 
��������� �������� ������ ����������� � ��:
/Application/DI.Reminder.Web/Web.config
/Service/DI.Reminder.Service/Web.config
������ ���������� ������ ������ � �������������. ����� ����� ��� �������� "Admin" � ������� "AdminAdmin".