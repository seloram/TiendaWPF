   M S B A M L   `   `   `  ����L  HSegundaEvaluacion, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:SegundaEvaluacionSegundaEvaluacion  5      +    %SegundaEvaluacion.ModificacionUsuario   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      1localclr-namespace:SegundaEvaluacion     5      -   5        $	��755��5   5     � 	Resources  5         listaUsuarios �� Source��%&   &	        �� 5
   
    67   �� 5      .��5      � -   5         �Name$ ventanaModUsuarios��5      $
��1186��6'   $��	0,0,0,-65q�64   $	��755��6G   $	��Top=�6T   ��5   
   �� 5       �� EndPoint$ 0.5,1&�6"    �� 
StartPoint$ 0.5,0&�63   .��5      �  � Color$ 	#FF00556E��6    � Offset$ 1Q�61   � 5      $ 	#FF006E8F��6   5      .+�5   
   �� -   5        $��titulo��5      $��MODIFICACIÓN DE USUARIOS��6     jugando#��[ 6A   $��48(�6b   �� 5   
   $��DATOS DEL USUARIO��6   $��53��6-   $��0,70,0,0q�69   $	��918��6L   #��[ 6X   $��34(�6y   �� 5   
   $
��Left=�6   $	��614��6/   $��
10,131,0,0q�6<   
���   �$	��Top=�6_   $
��1166��6w    J� Opacity$	 0.4Q�6�   ��5      �� 5      $ 0.5,1&�6&   $ 0.5,0&�67   .��5      � $ Black��6#   $ 0Q�61   � 5      $ White��6#   $ 1Q�61   5      5      &   System.Windows.Controls.DataGrid  5%   
   -   5          Name$
 data��5%      $��10,205,0,315q�6a   $
��1166��6w    	 grids#��[ 6�     HeaderStyle	   ColumnHeaderStyle#	 [ 5&      ��5%   "   �� 
 �� Source#
 [     Columns 5'      0  *System.Windows.Controls.DataGridTextColumn  5(      ,  &System.Windows.Controls.DataGridColumn   
IsReadOnly .  	 datos   	CellStyle# [ 6W      
Foreground
 �   �   Width5  /System.Windows.Controls.DataGridLengthConverter$ * 6�      Header$ Nombre��6�   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6%   ��  �� Path$ Nombre�6�     5)       . # [ 6Z   
 �   �$ * 6�   $ 	Apellidos��6�    6%   �� $ 	Apellidos�6�     5*       . # [ 6T   
 �   �$ * 6�   $	 Dni��6�    6%   �� $	 Dni�6�     5+       . # [ 6V   
 �����$ * 6�   $ Email��6�    6%   �� $ Email�6�   51      �� 52   
   -   5         ��Name$ filtrarNombre��52        t_inser_usu#��[ 6)   $�� ��6N   $��
63,137,0,0q�6V   $	��222��6j   6�   �� 53   
   -   5        $ 
filtrarDni��53      #��[ 6&   $�� ��6K   $��339,137,0,0q�6S   $	��222��6h   6�   �� 54   
   -   5        $ filtrarApellidos��54      #��[ 6,   $�� ��6Q   $��626,137,63,0q�6Y   $	��222��6o   6�   �� 55   
   -   5        $ filtrarEmail��55      #��[ 6(   $�� ��6M   $��901,137,63,0q�6U   $	��222��6k   6�   �� 56   
   -	   5          botones#��[ 56      $��40(�62     brillosTextos#��[ 6@   $��Eliminar usuario��6l   $��
18,692,0,0q�57      $	��324��6%   $��50��61   6=   �� 58   
   -
   5        $��botonModificar��58      #��[ 6'   $��40(�6H   #��[ 6V   $��Modificar Usuario��6�   $��845,692,0,0q�59      $	��324��6"   $��50��6.   6:   �� 5:   
   -   5        $��	resultado��5:      $
��Left=�6#   $��47��6>   $��362,695,0,0q�6J    �� TextWrapping$
 Wrap=�6_   $��TextBox��6s   $	��Top=�5;      $	��465��6*   66   � 5<   
   -   5         �Name$ datosUsuario��5<        degOperaciones#��[ 6*   $
��Left=�6X   $	��242��6s   $��
10,445,0,0q�5=      $	��Top=�6(   $
��1159��6@   5>      �� 5?   
   -   5        $ filtrarNombreCapa��5?      $��Nombre��6-   #��[ 6;   $��
63,137,0,0q�6`   $	��222��6t   6�   �� 5@   
   -   5        $ filtrarDniCapa��5@      #��[ 6*   $	��Dni��6O   $��339,137,0,0q�6Z   $	��222��6o   6  �� 5A   
   -   5        $ filtrarApellidosCapa��5A      #��[ 60   $��	Apellidos��6U   $��626,137,0,0q�6f   $	��222��6{   6%  �� 5B   
   -   5        $ filtrarEmailCapa��5B      #��[ 6,   $��Email��6Q   $��901,137,0,0q�6^   $	��222��6s   6  5C      5D      