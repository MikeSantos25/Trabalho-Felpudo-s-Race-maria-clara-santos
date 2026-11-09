# Trabalho-Felpudo's-Race-maria-clara-santos
Link do Jogo no Unity Play: https://play.unity.com/en/games/ba573270-614c-4d40-8d70-9542191889cb/felpudos-race-aula

Sobre o Projeto

O vilão Uruca sequestrou a Fofura e a levou para o topo de uma montanha cheia de perigos! Movido pelo amor e pela coragem, o herói Fofuxo pega seu carro e parte em uma jornada eletrizante para resgatar sua amada. O jogo mistura desvio de obstáculos e velocidade: o jogador salta e se agacha no momento certo para desviar dos capangas e criaturas enviadas por Uruca, enquanto coleta frutas espalhadas pela estrada para manter a energia. Ao alcançar 1000 pontos, Fofuxo derrota os lesmos de Uruca e resgata a Fofura. Colidir com qualquer inimigo significa derrota.

Como Jogar
Barra de espaço / Seta para Cima / W / Botão esquerdo do mouse: Faz o Fofuxo saltar
Seta para Baixo / S: Faz o Fofuxo agachar
Tecla "R": Reinicia o jogo após vitória ou derrota
Funcionalidades Implementadas
Controle do Jogador (PlayerController)
Movimentação com pulo e agachamento com Rigidbody2D e ajuste dinâmico da hitbox via BoxCollider2D.
Troca fluida de animações quadro a quadro (corrida, pulo, agachamento e queda) através do componente SpriteAnimator.
Condição de derrota ao colidir com qualquer obstáculo ou inimigo (detecção por componente/tags Enemy).
Coleta de frutas que incrementa a pontuação acumulada do jogador.
Reinício da cena com a tecla R.

Gerador de Obstáculos (Spawner)
Geração aleatória de inimigos e frutas a partir de prefabs em intervalos de tempo configuráveis.
Posicionamento em alturas variadas (no chão para pulo e no ar para agachamento).
Movimentação contínua dos objetos gerados da direita para a esquerda através do script Mover, com destruição automática ao saírem do limite da câmera.
Gerenciador do Jogo (GameManager)
Controle do estado de jogo (jogando, vitória e derrota).
Aceleração progressiva da velocidade do cenário à medida que o jogador avança.
Cálculo de pontuação baseado na distância percorrida e quantidade de frutas coletadas.
Condição de vitória acionada automaticamente ao atingir 1000 pontos, exibindo a mensagem "VOCÊ RESGATOU A FOFURA!" e os detalhes da partida na interface UI.
Salva o recorde de melhor pontuação (BestKey) no PlayerPrefs.
Interface do Usuário (GameInput & Canvas UI)
Abstração simples de inputs suportando teclado e cliques do mouse.
Atualização em tempo real do placar de pontos e frutas na tela.
Painel único de Game Over/Vitória que alterna dinamicamente seu conteúdo dependendo do resultado da partida.

Publicação
Build em WebGL, publicado no Unity Play.

Tecnologias
Unity
C#
UI Text
Universal Render Pipeline (URP 2D)

Aluna: Maria Clara Santos
