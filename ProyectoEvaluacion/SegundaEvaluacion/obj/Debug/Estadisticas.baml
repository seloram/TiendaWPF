   M S B A M L   `   `   `  ����L  HSegundaEvaluacion, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:SegundaEvaluacionSegundaEvaluacion  5      U QLiveCharts.Wpf, Version=0.9.7.0, Culture=neutral, PublicKeyToken=0bc1f845d1ebb8dfG4clr-namespace:LiveCharts.Wpf;assembly=LiveCharts.WpfLiveCharts.Wpf 5      O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem 5	      $    SegundaEvaluacion.Estadisticas   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      1localclr-namespace:SegundaEvaluacion     5      Dlvc4clr-namespace:LiveCharts.Wpf;assembly=LiveCharts.Wpf     5      9System&clr-namespace:System;assembly=mscorlib     5	      .��5      � ��5   
   �� 5        �� EndPoint$  0.5,1&�6"    �� 
StartPoint$ 0.5,0&�63   .��5      �  � Color$ 	#FF00556E��6    � Offset$ 1Q�61   � 5      $ 	#FF006E8F��6   5      .+�5   
   �� -   5         ��Name$ titulo��5      $��NUEVO PEDIDO��6       jugando#��[  67   $��48(�6X   $��	10,8,10,0q�6f   �� 5   
   $
��Left=�6   $	��595��60   $��
10,150,0,0q�6=   
���   �$	��Top=�6`   $	��574��6x   ��5      �� 5      $  0.7,1&�6&   $ 0.5,0&�67   .��5      � $ Black��6#   $ 0Q�61   � 5      $ 	#FF2BE8F1��6#   $ 1Q�65   5      5      �� 5   
   $
��Left=�6   $	��595��60   $��589,150,0,0q�6=   
���   �$	��Top=�6a   $	��587��6y   ��5      �� 5      $  0.5,1&�6&   $ 0.7,0&�67   .��5       � $ Black��6#   $ 0Q�61   � 5!      $ 	#FF2BE8F1��6#   $ 1Q�65   5"      5$      (  "System.Windows.Controls.DatePicker  5%   
   -   5          Name$ 
pedidosDia��5%      $
��Left=�6*     botonAceptar#���  6E   $��	10,82,0,0q�6q   $	��Top=�6�   $��56��5&      $	��368��6"   $��36(�6.   $��Center=�6<   $��Center=�6^   $��Franklin Gothic Medium)�5'      
�������$��0q�6M   $��0q�6a   #  LiveCharts.Wpf.CartesianChart  5(   
   -   5          Name$ chart1��5(      &   LiveCharts.Wpf.Charts.Base.Chart   LegendLocation$ Right=�5)      $��0��5*      $��32,170,624,29q�6*   
������    Series 5.        LiveCharts.Wpf.LineSeries  -   5        	  Name$	 serieP��5.   !     LiveCharts.Wpf.Series
   Title$
 Pedidos��61      StrokeThickness$ 5Q�6A      AxisY 51        LiveCharts.Wpf.Axis     MaxValue$ 40Q�6      MinValue$ 0Q�6)      Title$ Pedidos��66      	Separator 52        LiveCharts.Wpf.Separator  53      {�.     Step$ 10Q�6:   55         AxisX 58        -   5          Name$ g2_eje_x��58      $	 Dia��6-      
Foreground
 �   �   FontSize$ 20Q�6L      
FontWeight$ 	ExtraBold"�6Z    5:        5;      {�.  $ 1Q�6:   5=      5?        LiveCharts.Wpf.PieChart  5@   
   -   5          Name$ pastel��5@      $ Right=�5A      
������ $��604,170,22,29q�6=   �� 5B   
   -   5          botones#��[ 5B        botonesMenu#��[ 62   
�������$��40(�6o   $��Calcular��5C      $
��Left=�6$   $��
394,82,0,0q�6?   $	��Top=�6S   $	��190��6k   $��56��6w   6�   5D      5E      