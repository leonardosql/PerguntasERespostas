   M S B A M L   `   `   `  ���� N  JPerguntasERespostas, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:PerguntasERespostasPerguntasERespostas  "    PerguntasERespostas.NovoJogo   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     3local!clr-namespace:PerguntasERespostas     -     � WindowStartupLocation$  CenterScreen=� � Title$ 	Novo Jogo��$��425.523��$��564.653��.��� .+��� -    ��Name$ listBoxCategorias��
������� �� SelectionMode$ Single=�$
��Left=�$��75��$��
30,177,0,0q�$	��Top=�$	��499�� � DataContext �� ���� �� -    ��Name$ label��$
��Bold"�$+��%Selecione as categorias das perguntas��$
��Left=�$��
30,146,0,0q�$	��Top=��� -   $ 
label_Copy��$
��Bold"�$��Selecione o modo de jogo:��$
��Left=�$��	30,10,0,0q�$	��Top=�	� -    	�Name$ radEliminativo�� 	� 	GroupName$
 jogo��$#��ELIMINAÇÃO POR PARTICIPANTE��$
��Left=�$��	30,49,0,0q�$	��Top=� d� 	IsChecked$
 TrueY�$��19��	� -   $ radMaisAcertos��$
 jogo��$��ELIMINAÇÃO POR PONTOS��$
��Left=�$��
287,49,0,0q�$	��Top=��� -   	 ��Name$	 label_Copy1��$
��Wrap=�$��10(�$
��Left=�$��	30,69,0,0q�$	��Top=�$��77��$	��233���� $$�A cada rodada um participante ���� $E�?é escolhido para responder a questão se responder errado ele ���� $�é���� $�
 eliminado���� $�	 do quiz.���� $� E���� $�	ste modo ���� $F�@vence a equipe que tiver mais jogadores ao terminar as perguntas���� $�	 ou todos���� $� seus ���� $�adversario ���� $�
eliminados���� $� na partida���� $�.���� -   $	 label_Copy2��$
��Wrap=�$��10(�$
��Left=�$��
287,69,0,0q�$	��Top=�$��56��$	��244���� $$�A cada rodada um participante ���� $�é���� $��� sorteado para responder uma pergunda se a resposta escolhida estiver correta é somado um ponto para sua equipe se errar a equipe não pontua. ���� -	   $ label_Copy3��$
��Bold"�$#��Número máximo de perguntas:��$
��Left=�$��
30,266,0,0q�$	��Top=��� -
   
 ��Name$
 txtNumPerguntas��$
��Left=�$��221,266,0,99q�$��50��$��center=�$��42���� -    ��Name$ 
btnInicias��$��Iniciar��$��30,323,28,22q�$��14(�$
��Bold"�