   M S B A M L   `   `   `  ����O  Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089I2clr-namespace:System.Collections;assembly=mscorlibSystem.Collections  5      1&clr-namespace:System;assembly=mscorlibSystem  5      M IFluent, Version=2.1.0.0, Culture=neutral, PublicKeyToken=3e436e32a8c5546f/$clr-namespace:Fluent;assembly=FluentFluent 5      E ALE2, Version=1.0.5702.30957, Culture=neutral, PublicKeyToken=null1clr-namespace:LEDX.ComponentsLEDX.Components 5      E AComponents, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullE1clr-namespace:LEDX.Components;assembly=ComponentsLEDX.Components 5      -clr-namespace:LEDX.CommandsLEDX.Commands 5	      1clr-namespace:LEDX.PropertiesLEDX.Properties 5
      'clr-namespace:LEDX.Utils
LEDX.Utils 5      +clr-namespace:LEDX.LoggingLEDX.Logging 5      1clr-namespace:LEDX.ConvertersLEDX.Converters 5         	LEDX.MWin   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      LsysCollection2clr-namespace:System.Collections;assembly=mscorlib     5      6sys&clr-namespace:System;assembly=mscorlib     5      3fl$clr-namespace:Fluent;assembly=Fluent     5      ,myclr-namespace:LEDX.Components     5      Aimp1clr-namespace:LEDX.Components;assembly=Components     5      )cclr-namespace:LEDX.Commands     5	      /propsclr-namespace:LEDX.Properties     5
      )utilclr-namespace:LEDX.Utils     5      *logclr-namespace:LEDX.Logging     5      .convclr-namespace:LEDX.Converters     5      -   5          Fluent.RibbonWindow   Name$  fmMain��5       � Title$ LEDX редактор 2��6    � Icon$ icon.ico��6.   $	��750��6>   $
��1000��6K    � WindowStartupLocation$ CenterScreen=�6X    � 	Resources 5      (  "LEDX.Converters.ActivatePanelFrame   actFrameCon(  "LEDX.Converters.ActivatePanelColor  actColorCon   LEDX.Converters.ChildCount  	actCldCnt  LEDX.Converters.NotNull  actNull#  LEDX.Converters.BoolToVisible  	actBolToV!  LEDX.Converters.ColorModels  cnColMod   LEDX.Converters.BoolToBool 
 cnBToB!	  LEDX.Logging.NotifyAppender  Log  familyCollection �� Source 
  System.Windows.Media.Fonts 
  SystemFontFamilies �� Source 	 contrCollection �� ElementName	 �� Path��%]  &	        &	      &	 &     &	 9     &	 L     &	 _     &	 r     &	 �     &	 �     &		 �       5      6     5      6     5      6     5      6     5      6     5      6     5      6   	  5      6   �� 5       62   �� # Z �� 5       61   �� $ fmMain��$	 Doc.Controller�5      
 J� InputBindings
 5      �� 5       �� Command$ c:Cmd.ErrorCommand��6    �� Gesture$ Ctrl+Shift+Alt+E��6,   �� 5!      $ c:Cmd.ExitCommand��6   $ Alt+X��6+   �� 5"      $ c:Cmd.NewCommand��6   $ Ctrl+N��6*   �� 5#      $ c:Cmd.OpenCommand��6   $ Ctrl+O��6+   �� 5$      $ c:Cmd.SaveCommand��6   $ Ctrl+S��6+   �� 5%      $ c:Cmd.SaveAsCommand��6   $ Ctrl+Shift+S��6-   �� 5&      $ c:Cmd.UndoCommand��6   $ Ctrl+Z��6+   �� 5'      $ c:Cmd.RedoCommand��6   $ Ctrl+Y��6+   �� 5(      $ c:Cmd.HelpCommand��6   $ F1��6+   �� 5*      $ c:Cmd.CopyFrame��6   $ Ctrl+C��6)   �� 5+      $ c:Cmd.CutFrame��6   $ Ctrl+X��6(   �� 5,      $ c:Cmd.CopyColor��6   $ Ctrl+Shift+C��6)   �� 5-      $ c:Cmd.PasteObject��6   $ Ctrl+V��6+   �� 5.      $ c:Cmd.PasteBefore��6   $ Ctrl+Shift+V��6+   �� 50      $ c:Cmd.AddFrame��6   $ Alt+F��6(   �� 51      $ c:Cmd.AddContr��6   $ Alt+C��6(   �� 52      $ c:Cmd.DelFrame��6   $	 Del��6(   �� 53      $ c:Cmd.DelContr��6   $ 	Shift+Del��6(   �� 54      $ c:Cmd.DefScale��6   $ Ctrl+0��6(   �� 55      $ c:Cmd.AddSample��6   $ Alt+Shift+S��6)   .��58      ]� ��. .+�59        Fluent.Ribbon  -   5        $��ribbon��59      $	��Top=�6      Menu 5;        Fluent.Backstage  5<        Fluent.RibbonControl   Header$ Меню��6   ���#3071da   Content. 5=         Fluent.BackstageTabControl  .@�5?        Fluent.Button  -   5        $��	btNewProj��5?       � ToolTip$* $Новый документ Ctrl + N��6#      Header$ 
Новый��6E   $��c:Cmd.NewCommand��6T      Icon$ Images\new_16.png��5@        5A      -   5        $��
btOpenProj��5A      $7 1Открыть файл прошивки Ctrl + O��6$   $ Открыть...��6M   $��c:Cmd.OpenCommand��6a   $ Images\folder_16.png��5B        5C      -   5        $��
btSaveProj��5C      $2 ,Сохранить документ Ctrl + S��6$   $ Сохранить...��6J   $��c:Cmd.SaveCommand��6`   $ Images\Save_16.png��5D        5E      $" Сохранить как...��6   $L FСохранить документ как копию Ctrl + Shift + S��6,   $��c:Cmd.SaveAsCommand��6d   $ Images\Save_16.png��5F        5G      $ 
Выход��6   $/ )Закрыть программу Alt + X��6!   $��c:Cmd.ExitCommand��6E   $ Images\Exit_16.png��6a   5H      5I         ToolBarItems 5L        Fluent.DropDownButton  5M         Icon$? 9/Fluent;Component/Themes/Office2010/images/morecolors.png��6$   %  Fluent.RibbonAttachedProperties   RibbonSizeDefinition$ Small��6e   $E ?Изменение цветовой темы программы��5N      .@�5O      m� $��
Синий��6    m� Command$ c:Cmd.SetTheme��6    m� CommandParameter$
 Blue��68    m� Icon$! Images\Themes\bluetheme.png��6P   m� 5P      $��Серебряный��6   $ c:Cmd.SetTheme��6$   $ Silver��6=   $# Images\Themes\silvertheme.png��6W   m� 5Q      $��Черный��6   $ c:Cmd.SetTheme��6    $ Black��69   $" Images\Themes\blacktheme.png��6R   5R        5S      $��c:Cmd.HelpCommand��6   $ Справка F1��68   $ Small��6M   $> 8/Fluent;Component/Themes/Office2010/images/helpsmall.png��5T   
      QuickAccessItems 5W         Fluent.QuickAccessMenuItem  5X       m� 	IsChecked .    Target 6   �� $ 
btOpenProj��6G     5Y       .  6   �� $ 
btSaveProj��6G      Tabs. 5\        Fluent.RibbonTabItem  -   5        $��tbEdit��5\   .      Header$" Редактирование��6      Groups. 5]        Fluent.RibbonGroupBox     Header$ Правка��6   .@�5^        Fluent.SplitButton  -   5        $��btUndo��5^          Header$  Отмена��6$   !   Command$! c:Cmd.UndoCommand��64   $A ;Отмена последнего действия Ctrl + Z��6P   "   	LargeIcon$" Images\Undo_32.png��6~   .@�5_        Fluent.MenuItem  $��Повтор��6   $ c:Cmd.RedoCommand��6$   $M GВернуть последнее отмененное действие��6@   $ Images\Redo_16.png��6p   5`        5a      -   5        $��btPasteFram��5a   %   $! c:Cmd.PasteObject��68   $  Вставить��6T   $ Вставка Ctrl + V��6f   $ Large��6�   $ " Images\Operat\Paste_32.png��5b   %   .@�5c        $!��Вставить слева��6   $ c:Cmd.PasteBefore��6,   $E ?Вставить перед выделенным фреймом��6H   $' !Images\Operat\arrow-down-left.png��6t   5d      5e        5g      -	   5        $��rgbContrProp��5g      $ Контроллер��6,   # � DataContext# 6@   �� $ fmMain��$	 ActCont�.@�5h        Fluent.RibbonToolBar  $   LayoutDefinitions$ 5i      *  $Fluent.RibbonToolBarLayoutDefinition  5j   	   %   Rows.% 5k   
     Fluent.RibbonToolBarRow  &   Children.& 5l   &   +  %Fluent.RibbonToolBarControlDefinition  '   Target$' spAddNew��6H   (   
RibbonSize$( Large=�6Z   5m      5n   
   )   Children.) 5q        -
   5        *  Name$* spAddNew��5q   )   $! c:Cmd.AddContr��6;   $  
Новый��6T   $ Large��6c   $> 8Добавить новый контроллер Alt + C��5r      $ Images\contradd_32.png��67   " 5s   	   �� 5t   
   $��Images\contradd_32.png��6   .@�5v   	     -   5        +  Name$+ btDel��5v      $��Удалить��6$   $ Images\delete_16.png��65   $ c:Cmd.DelContr��6Q   $F @Удалить выбраный контроллер Shift + Del��5x      {�5w      �� $ fmMain��$	 ActCont�, �� 	Converter#, [ 5x      5y   	   5z        5|      {�6   �� $ fmMain��$	 ActCont�#, [ $ 5}        5~   	   .% 5   
     .& 5�   &     $' 	tbConName��6H   -   Width$	- 200Q�6[   $( Large=�6g   5�        5�   
   .& 5�   &     $' 	tbConNumb��6H   $- 90Q�6[   $( Large=�6f     5�   &   $' 	tbConButt��6H   $	- 110Q�6\   $( Large=�6h   5�      5�   
   .) 5�        Fluent.TextBox  -   5        $��	tbConName��5�      $ 
Текст��6$   .   Text. 63   �� *Title+/ �� Mode$/ TwoWay=�  Fluent.Spinner  5�      -   5        $��	tbConNumb��5�      $ 
Номер��6$   $��Normal&�63   0   Format$0 F0��6H   1   	Increment$1 1Q�6T   2   Maximum$2 50Q�6b   3   Minimum$3 1Q�6o   4   Value4 5�   
   �� *Number+$/ TwoWay=�  5�      -   5        $��	tbConButt��5�      $ Программа��6$   $��Normal&�67   $0 F0��6L   $1 1Q�6X   $2 5Q�6f   $3 1Q�6r   4 5�   
   �� *Button+$/ TwoWay=�5�      5�        5�      -   5        $��rgbFrameProp��5�      $ 
Фрейм��6,   .@�5�        {�5�      �� $ fmMain��$	 ActFrm.Count�#, [  $ 5�        5�   	   .% 5�   
     .& 5�   &     $' spTime��6H   $( Large=�6X   $	- 115Q�6�   5�        5�   
   .& 5�   &     $' 
btCopyFram��6H   $( Small=�6\     5�   &   $' 	btCutFram��6H   $( Small=�6[     5�   &   $' 	btDelFram��6H   $( Small=�6[     5�   &   $' 
btToSample��6H   $( Small=�6\   5�      5�   
   .) 5�        -   5        5  Name$5 spTime��5�      $ 
Время��6#   $	3 .05Q�62   $
2 6500Q�6@   $��Normal&�6O   $0 F2��6d   $	1 1.0Q�6p   4 5�   
   �� *Length+$/ TwoWay=�  5�      -   5        6  Name$6 
btCopyFram��5�      $��c:Cmd.CopyFrame��6&   $. (Копировать фрейм Ctrl + C��6@   $ Images\Operat\Copy_32.png��6d     5�      -   5        $6 	btCutFram��5�      $��c:Cmd.CutFrame��6%   $* $Вырезать фрейм Ctrl + X��6>   $ Images\Operat\Cut_16.png��6`     5�      -   5        $6 	btDelFram��5�      $��c:Cmd.DelFrame��6%   $��	15,0,15,0q�6>   $# Удалить фрейм Del��6Q   $ Images\delete_16.png��6m     5�      -   5        $6 
btToSample��5�      $��c:Cmd.AddSample��6&   $��False��6@   $4 .Добавить в шаблоны Alt+Shift+S��5�      $ Images\colors_16.png��65   5�        5�      $ 5�        5�   	   .% 5�   
     .& 5�   &     $' spbtFSet��6H   $( Large=�6Z   5�      5�   
   .) 5�        -   5        $��spbtFSet��5�   $   $  Вид��6   .@�5�   	   �� 7 �� Orientation$7 Vertical=�6   .+�5�   
   �� $7 
Horizontal=�6   .+�5�   &     Fluent.ToggleButton  8   Header$8 Разделители��66   9   Icon$ 9 Images\Themes\Split_16.png��6K   $ Middle��6m   : d� 	IsChecked: 5�      ��   LEDX.Properties.Settings;   Default# Z; $	 Split�$/ TwoWay=�#, [   5�   &   $8 Сглаживание��66   $ 9 Images\Themes\Round_16.png��6K   $ Middle��6m   : 5�      �� # Z; $	 Round�$/ TwoWay=�#, [ 5�      !  LEDX.Components.ColorCanvas  5�   
   $��
0,0,0,-117q�6   <   SelectedColor< 5�      �� # Z; $	 	BordColor�$/ TwoWay=�#, [ 5�   
   " 5�   	   �� 5�   
   $��Images\FrameSets_32.png��6   $��32��61   $��32��6<   5�   	   5�      5�        5�      -   5        $��rgbColorProp��5�      $# Настройки цвета��6,   {�5�      �� $ fmMain��$	 ActFrm.Count�#, [ .@�5�        $ 5�        5�   	   .% 5�   
     .& 5�   &     $' 
btBegColor��6H   $( Large=�6\   5�      5�   
   .) 5�        -   5        $6 
btBegColor��5�      $ Начало��6&   $��c:Cmd.SetColorBeg��66   $; 5Задать начальный цвет фрейма��6R   =   	LargeIcon= 5�   	   �� 5�   
   -   5        > ��Name$> rctBegClolor��5�      $��32��6*   $��32��65   ? �� RadiusX$? 3��6A   @ �� RadiusY$@ 3��6M   
�������$��2��6h   ��5�      �� 5�      A �� ColorA 6   �� *
BegColor+5�      5�   	   5�        5�      $ 5�        5�   	   .% 5�   
     .& 5�   &     $' btCopyColor��6H   $( Small=�6]   5�        5�   
   .& 5�   &     $' 
btGetColor��6H   $( Small=�6\   5�      5�   
   .) 5�        -   5        $6 btCopyColor��5�      $��c:Cmd.SetSolidColor��6(   $ Images\right_16.png��6F   ${ uУстановить конечный цвет фрейма равный начальному (Ctrl наоборот)��5�        5�      -   5        $6 
btGetColor��5�      $��c:Cmd.SetLrColor��6&   $* $Взять цвета соседей��6A   $# Images\Operat\exchange_16.png��6_   5�        5�      $ 5�        5�   	   .% 5�   
     .& 5�   &     $' 
btEndColor��6H   $( Large=�6\   5�      5�   
   .) 5�        -   5        $6 
btEndColor��5�      $ 
Конец��6&   $��c:Cmd.SetColorEnd��65   $9 3Задать конечный цвет фрейма��6Q   = 5�   	   �� 5�   
   -   5        $> rctEndClolor��5�      $��32��6*   $��32��65   $? 3��6A   $@ 3��6M   
�������$��2��6h   ��5�      �� 5�      A 6   �� *
EndColor+5�      5�   	   5�        5      $ 5       5  	   .% 5  
     .& 5  &     $' btCopy��6H   $( Large=�6X   5     5  
   .) 5	       -   5        $6 btCopy��5	     $ 
Копия��6"   $��c:Cmd.CopyColor��61   $G AКопировать цветовую схему Ctrl + Shift + C��6K   = 5
  	   �� 5  
   $��32��6   $��32��6   $��Images\Operat\Copy_32.png��6'   5  	   5     5       5     $ Шаблоны��6   {�6)   �� $ fmMain��$	 ActCont�#, [ .@�5        Fluent.InRibbonGallery   -   5        B   Name$B grSampl��5     C    MinItemsInRow$C 4��6+   D    MaxItemsInRow$D 4��6=   $	��250��6O   E    	ItemWidth$E 58Q�6[   F    
ItemHeight$F 58Q�6j   G    
ResizeMode$
G Both=�6z   H    Icon$H Images\colors_16.png��6�   ��5     �� 5  	   .R�5  
   �� $��2q�62   ���#3071daI �� CornerRadius$I 5��6\   ��6   �� *	IconLarge+._�5     �� J ��Name$J StopText��6   $��18(�6E   $��Stretch=�6S   $��Stretch=�5     $��Tahoma)�6-   K �� Padding$K 35,27q�6A   $
��Bold"�6Q   
���������5  '   �� *Frames.Count+5  c   L � ContextMenuL 5     �� 5     ��5     -!  'System.Windows.Data.CompositeCollection!  5     m� 5     $��Удалить��6   $ c:Cmd.DelSample��6)   $ Images\delete_16.png��6C   5     5      
 5"     d� 5#     M d� Gesture$M LeftDoubleClicka�6   $ c:Cmd.InsertSample��63   5%     5&  
   5(     5)     5*       5,     -   5        $��tbPalyer��5,  ,   $ Предпросмотр��6   y�5-     �� $ icContr��$	 ActualHeight�#, [ . 5.       $ Вставить��6   .@�5/       $ Фон��6    $ Large��6-   $��c:Cmd.OpenBackground��6f   $"= Images\Import\Picture_32.png��6�   $� �Фон нужен для установки области экспорта (для выделения фона нажать Ctrl + LMB)��50       51     $ PNG альфа канал��6    $��c:Cmd.OpenLigth��69   $ Large��6S   $= Images\Import\Png_32.png��6�     52     $ Конвертер��6    $��c:Cmd.ConvertLigth��63   $ Large��6P   $"= Images\Import\Convert_32.png��6�   $T NСоздать из изображения карту освещенности��53     y�54     �� # Z; $	 Develop�#, [   55     $ 
Текст��6    $��c:Cmd.InsertLabel��6/   $ Large��6K   $= Images\Text_32.png��6�   y�56     �� # Z; $	 TestMode�#, [ 57       58     $ Экспорт��6   {�6)   �� $ iCanv��$	 IsBackLoaded�.@�59       $	 PNG��6    $ Large��6-   $��c:Cmd.ExportToPng��6f   $= Images\Import\Png_32.png��6�     5:     $ 
Видео��6    $ Large��6/   $��c:Cmd.ExportToAvi��6h   $ = Images\Import\Video_32.png��6�   5;       5<     $/ )Инструменты рисования��6   y�5=     �� # Z; $	 TestMode�#, [ .@�5>       $��c:Cmd.EditMode��6    $��1��69   $ Круг��6N   $ Large��6\   $= Images\Draw\Circle_32.png��5?       5@     $��c:Cmd.EditMode��6    $��2��69   $ Квадрат��6N   $ Large��6_   $= Images\Draw\Square_32.png��5A       5B     N   	GroupName$N canvas��6&   $��c:Cmd.EditMode��69   $��3��6R   $8 
Линия��6g   $ Large��6v   O   	LargeIcon$O Images\Draw\line_32.png��5C       5D     $N canvas��6&   $��c:Cmd.EditMode��69   $��4��6R   $8 Кривая��6g   $ Large��6w   $O Images\Draw\Pencil_32.png��5E       5F     $N canvas��6&   $��c:Cmd.EditMode��69   $��5��6R   $8 Выбрать��6g   $ Large��6x   $O Images\Draw\Select_32.png��5G     $
: TrueY�61     5H     $N canvas��6&   $��c:Cmd.EditMode��69   $��6��6R   $8 Стереть��6g   $ Large��6x   $O Images\Draw\Eraser_32.png��5I     5J       5R     -    5        $��rgbPropLayer��5R     $ Слой��6,   $y�	Collapsed=�6:   .@�5S       $ 5T       5U  	   .% 5V  
     .& 5W  &     $' 
btLayerTop��6H   $( Large=�6\     5X  &   $' btLayerDown��6H   $( Large=�6]     5Y  &   $' btDelLay��6H   $( Large=�6Z   5Z     5[  
   .) 5]       -!   5        $6 btLayerDown��5]     $��c:Cmd.LayerDown��6'   $ Ниже��6A   $ = Images\Draw\LayerDw_32.png��6O     5^     -"   5        $6 
btLayerTop��5^     $��c:Cmd.LayerUp��6&   $ Выше��6>   = 5_  	   �� 5`  
   $ ��Images\Draw\LayerUp_32.png��6   $��32��64   $��32��6?   5b  	     5c     -#   5        $��btDelLay��5c     $ Удалить��6"   $��c:Cmd.LayerDelete��63   $= Images\delete_16.png��6O   5d     5e       5g     -$   5        $��
rgbPropPng��5g     $ Свойства��6*   $y�	Collapsed=�6<   .@�5h       $ 5i       5j  	   .% 5k  
     .& 5l  &     $' cbController��6H   $( Large=�6^   $	- 170Q�6�   5m       5n  
   .& 5o  &     $' lgWidth��6H   $( Large=�6Y   $	- 100Q�6�     5p  &   $' lgHeight��6H   $( Large=�6Z   $- 60Q�6�   5q     5r  
   .) 5t     "  Fluent.ComboBox"  -%   5        P " Name$P cbController��5t     Q "  Header$Q Контроллер��6*   R �� 
IsEditableR .  S �� SelectedItemS 6>   �� *Contr+$/ TwoWay=���5u     �� # [	 ��5v  	   �� 5w  
   .R�5x     �� ��5y     `� 5z     T �� StringFormat$T  {0}: {1},{2}��6   .1�5{     �� $	 Title�6   �� 5|     $	 Number�6   �� 5}     $	 Button�6   5~     5�     5�     5�  	     5�     -&   5        $��lgWidth��5�     $ Размеры: ��6"   . 65   �� *Width+$T F0��  5�     -'   5        $��lgHeight��5�     $	  x ��6#   . 60   �� *Height+$T F0��5�     5�       5�     -(   5        $��rgbPropText��5�     $ Свойства��6+   $y�	Collapsed=�6=   .@�5�       $ 5�       5�  	   .% 5�  
     .& 5�       $' 	lbtemTest��6-     5�  &   $' temTest��6H   $( Large=�6Y   $- 40Q�6�     5�  &   $' 
spTextSize��6H   $( Large=�6\   $	- 100Q�6�   5�       5�  
   .& 5�  &     $' cbFontFamaly��6H   $( Large=�6^   $	- 170Q�6�   5�     5�  
   .) 5�     �� -)   5        U ��Name$U 	lbtemTest��5�     $��
Текст��6!   )#  #LEDX.Components.MultiLineTextEditor#  5�     -*   5        V # Name$V temTest��5�      W #  TextW 61   �� *Text+$/ TwoWay=�  5�     -+   5        $5 
spTextSize��5�     $ Размер��6'   $3 10Q�67   $2 50Q�6D   $��Normal&�6Q   $0 F0��6f   $1 1Q�6r   4 5�  
   �� *
FontSize+$/ TwoWay=�"  5�     -,   5        $P cbFontFamaly��5�  &   R .  $Q 
Шрифт��6O   $ Large��6^   ��5�     �� # [ S 5�     �� *
FontFamily+$/ TwoWay=���5�  	   �� 5�  
   .R�5�     �� $��14(�6K   ��6   �� *Source+��6-   �� *Source+6K   5�     5�  	   5�       5�     $  Цвет��6   $( "Задать цвет текста��6$   .@�5�       $��
0,0,0,-117q�6C   < 6   �� *Foreground.Color+6C   " 5�     �� 5�  	   --   5        $> rctTextClolor��5�     $��32��6*   $��32��65   $? 3��6A   $@ 3��6M   
�������$��2��6h   ��5�  
   �� 5�     A 6   �� *Foreground.Color+5�  
   5�     5�     5�       5�     -.   5        $��tbFlash��5�  (   $ Прошивка��6   y�5�     �� $ icContr��$	 ActualHeight�#, [ . 5�       $ Настройки��6   # 6+   �� $ fmMain��$	 	Doc.Flash�.@�5�       $ 5�       5�  	   .% 5�  
     .& 5�       $' lbPort��6-   $- 60Q�6=     5�     $' 	cbComName��6-   $- 70Q�6@     5�  &   $' btRefrPorts��6H   $( Small=�6]   5�       5�  
   .& 5�       $' lbSpeed��6-   $- 60Q�6>     5�     $' 	cbComRate��6-   $- 90Q�6@   5�     5�  
   .) 5�     �� -/   5        $U lbPort��5�     $��	Порт:��6   "  5�     -0   5        $P 	cbComName��5�     R .  X �� SelectedValueX 6:   �� *Port+$/ OneWayToSource=���5�  	   �� 5�  
   .R�5�     �� ��6   �� 5�     5�  	   �� 5�     -1   5        $U lbSpeed��5�     $��Скорость:��6   "  5�     -2   5        $P 	cbComRate��5�     R .  Y �� SelectedIndex$Y 0��6:   X 5�     �� *Speed+$/ TwoWay=�.@�5�  	   ��
115200��6   ��5�  	   9600��6   ��5�  	   4800��6   5�  	     5�     -3   5        $6 btRefrPorts��5�     $��c:Cmd.RefreshController��6'   $< 6Обновить список контроллеров��6I   $ Images\Refresh_16.png��5�     5�       5�     $ 5�       5�  	   .% 5�  
     .& 5�  &     $' tbSync��6H   $( Large=�6X   $- 50Q�6�   5�     5�  
   .) 5�       -4   5        $��tbSync��5�     $ 8 Синхронизация��6&   : 6=   �� *IsSync+$/ TwoWay=�O 5�  	   �� 5�  
   $��32��6   $��32��6   $ ��Images\Operat\Align_32.png��6'   5�  	   5�     5�       5�     $ Контроллеры��6   .@�5�       $ Прошить��6    $ Large��61   $��c:Cmd.FlashController��6j   $ Images\flush_32.png��6�   $S MЗапустить прошивку выбраных контроллеров��5�     $= Images\flush_32.png��6>   {�5�     �� $ 	cbComName��$	 SelectedItem�#, [   5�     $ Вкл/Выкл��6    $ Large��62   $��c:Cmd.PowerController��6k   $8 2Включить первый контроллер��6�   $= Images\Shutdown_32.png��5�     $ Images\Shutdown_32.png��6.   5�     5�     5�     $  Fluent.StatusBar$  5�     $��Bottom=�6   $��25��6)   .@�5�     %  Fluent.StatusBarItem%  $
��Left=�6   .��5�       -5   5        Z  Name$Z 
tgbShowLog��5�  "   $ Small��66   $9 Images\Log_16.png��6o   $ Показать лог��6�   : 5�  
   �� # Z; $	 ShowLog�5�     %  5�     $
��Left=�6   .��5�     �� $K 0,3,0,0q�6I   # 6   �� $ fmMain��$	 ActCont���5�     `� 5�     $eT _Название `{0}`, номер {1}, кнопка {2}, длина {3:F2}, фреймов {4}��6   .1�5   	   �� $	 Title�6   [ �� FallbackValue$[  ��6   �� 5  	   $	 Number�6   $[  ��6   �� 5  	   $	 Button�6   $[  ��6   �� 5  	   $	 Length�6   $[  ��6   �� 5  	   $	 Frames.Count�6   $[  ��6%   5  	   5     5     %  5
     $
��Left=�6   .��5     �� -6   5        \ ��Name$\ 
spProgress��5     $7 
Horizontal=�6%   $y�	Collapsed=�6>   .+�5     �� $-��'Выполнение операции: ��6   � 5     $	��120��6   ] � IsIndeterminate] . 6   5     5     %  5     $��Right=�6   .��5     �� -7   5        ^ ��Name$^ uiScaleSlider��5     $	��150��6$    
 ZoomSliderStyle#��[
 60   _ � Minimum$	_ 0.1Q�6Y   ` � Maximum$	` 2.0Q�6g   a �� AutoToolTipPlacement$a TopLeft=�5     b �� AutoToolTipPrecision$b 1��6&   c � LargeChange$	c 0.2Q�6?   d � SmallChange$	d 0.1Q�6Q   e � Value$e 1Q�6c   5     5     5     � 5     f � RowDefinitionsf 5     �� 5     �� 5     $
��Auto �6   .+�5     �� $��0��6   $I 3��6   ���	#66D6E2F8$��2q�6B   ._�5     � f 5     �� 5      $
��Auto �6   �� 5!     �� 5"     $
��Auto �6   g � ColumnDefinitionsg 5$     �� 5%     $
��Auto �6   �� 5&     $��1 �6   �� 5'     .+�5)     &  LEDX.Components.PlayScale&  -8   5        h & Name$h uiMan��5)     $��0��6$   $��0��64   $��0,0,0,1q�6A   i &  IsPlayi 5*     �� '  LEDX.Utils.PlayerUtilj '  I# Zj $	 IsPlay�$/ TwoWay=�k &  Scalek 5+     �� $ uiScaleSlider��$	 Value�$/ TwoWay=�y�5,     �� $ tbFlash��$	 
IsSelected�l �� ConverterParameter$l False��#, [ � 5-     $��1��6   $��0��6   
���   �6)   �� 5.     -9   5        m ��Name$m svSearch��5.     $��2��6&   $��0��66   $��Hidden=�6C   $��Disabled=�5/     ���	#FFC4C4C4$��0,0,0,1q�6I   .��50      (  LEDX.Components.TimeSlider(  -:   5        n ( Name$n sldTime��50     $_ 0Q�6(   $` 1Q�64   $
d 0.02Q�6@   o �� TickFrequency$
o 0.01Q�6S   $��0��6h   $e 0Q�51     $
��Left=�6   y�52     �� $ tbFlash��$	 
IsSelected�$l False��#, [ 55     �� 56     -;   5        $��scroll��56  E   $��0��6   $��1��6$   $��3��61   $
��Auto=�6S   $��Disabled=�57     $��Stretch=�64   .��58     � g 59  	   �� 5:  
   $	��220 �6   �� 5;  
   $��1 �6   �� 5<  
   .+�5?  	   �� -<   5        p ��Name$p icContr��5?  &   $��0��6   $	��Top=�5@     ��5?  7   �� $ fmMain��$	 Doc.Controller���5A  
   �� 5B     .R�5C     ")  LEDX.Components.UiController)  # 6   �� 5D     5F  
   � 5H  	   $��1��6   
���   �6   �� 5I  	   -=   5        $m svTracks��5I     $��2��6(   $��Hidden=�68   $��Disabled=�5J     .��5K  
   � .+�5L     �� ->   5        $p icTrack��5L  (   $��0��6   $��Stretch=�69   $
��Auto��6W   ��5M     �� $ fmMain��$	 Doc.Controller�y�5N     �� $ tbPalyer��$	 
IsSelected�$l False��#, [ ��5O     �� 5P     .R�5Q     *  LEDX.Components.UiTrack*  # 6   �� 5R     5T     +  LEDX.Components.IncRender+  5V     -?   5        q + Name$q iCanv��5V     y�6R   �� $ tbPalyer��$	 
IsSelected�#, [ 5W     5X  
   5Z  	   5[     � 5\     $��0��6   $��2��6   � 5]     $��1��6   $��2��6   �� 5^     -@   5        $m mainScroller��5^  1   $��2��6   $��2��6$   $
��Auto=�6G   $��Disabled=�5_     .��5`     � ��6   �� $ icTrack��$	 ActualWidth�5a     5c     5d     �� 5e     $��1��6   $I 3��6   ���	#66D6E2F8$��2q�6B   $��80��6V   y�5f     �� $ 
tgbShowLog��$	 	IsChecked�#, [ ._�5g     �� -A   5        $��svLog��5g     $
��Auto=�6    $��Disabled=�6C   .��5h     �� -B   5        $��tbLog��5h     ## [ 6   r �� 
IsReadOnlyr . s �� AcceptsReturns . t �� TextWrapping$
t Wrap=�6�   ��6?   �� *Appender.Notification+$/ Default=�6�   5i     5j     5k     5l     5n     