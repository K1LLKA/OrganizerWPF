   M S B A M L   `   `   `  ����G  COrganizerWPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:OrganizerWPFOrganizerWPF  5   	       OrganizerWPF.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ,localclr-namespace:OrganizerWPF     5   	     � Title$  
MainWindow��5   	   $	��450��6   $	��800��6)   .��5	      �  � ColumnDefinitions 5
   
   �� 5      $��* �6   �� 5      $��2* �6   .+�5   
   �� $��0��6   $��10q�6%   .+�5      �� $$��Название задачи:��6   �� 5      -   5         ��Name$ TitleTextBox��5      �� 5      $��Описание:��6   �� 5      -   5        $ DescriptionTextBox��5      $��80��62    �� TextWrapping$
 Wrap=�6>   �� 5      $$��Дата выполнения:��6   (  "System.Windows.Controls.DatePicker  5      -   5          Name$ DueDateDatePicker��5      �� 5      $��Приоритет:��6   �� 5      -   5         ��Name$ PriorityComboBox��5      5      �� 5      -   5        $��Добавить��5      $��0,10,0,0q�6@   �� 5      -   5         ��Name$ 
SaveButton��5   )   $��Сохранить��6   $��0,5,0,0q�6V   {�.  6g   �� 5       -   5        $ DeleteButton��5    '   $��Удалить��6   $��0,5,0,0q�6X   {�.  6i   5"      �� 5$   
   -   5         ��Name$ TasksListView��5$      $��1��6*   $��10q�6:    �� View 5%      �� 5&      .P�5'      �� $��Название��6%   	 �� Width$		 150��67   
 �� DisplayMemberBinding
 6C   �� *Title+�� 5(      $��Дата��6%   $		 100��63   
 6?   �� *	DueDate+ �� StringFormat$ 
dd.MM.yyyy���� 5)      $��Приоритет��6%   $	 80��68   
 6C   �� *
Priority+5*      5,      5-      5.      