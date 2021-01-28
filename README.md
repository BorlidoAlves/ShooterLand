## ShooterLand

ShooterLand é um jogo de computador desenvolvido em âmbito académico para a unidade curricular Laboratório de Desenvolvimento de Software do curso Licenciatura em Engenharia Informática.
  O jogo contará com um modo *singleplayer* em que o objetivo será sobreviver ao maior número de rondas possíveis , sendo que para isso, o jogador contará com a ajuda de *boosts*
  que irão aparecer no mapa uma vez a cada ronda. Na transição de uma ronda para outra, será apresentado um menu onde o jogador poderá aumentar um atributo do seu personagem.
  De forma a criar diversidade na jogabilidade, existem 3 personagens diferentes.
  Para além disso, existe também um modo *multiplayer* onde dois jogadores competem entre si ,sendo o vencedor o jogador que eliminar o outro.
  Visando tornar o jogo mais apelativo e aliciante para os nossos jogadores, foram implementadas tabelas classificativas e um sistema de conquistas. Estas duas funcionalidades podem ser consultadas no *website* do projeto.
  
  
## Tecnologias utilizadas
   - Monogame
   - .NET Core
   - Angular
   - Bootstrap
   - SQL Server
   - Xunit
   - Jasmine & Karma

## Instruções para correr o projeto
### API
Qualquer funcionalidade requer que a Rest Api esteja disponível, desta forma, deverá seguir os seguintes passos:
- Entrar na pasta shooterland
- Entrar na pasta API
- Entrar na pasta shooterlandWebBack
- Abrir a linha de comandos e introduzir `dotnet run`

### Jogo
   - Entrar na pasta shooterland
   - Entrar na pasta ShooterLand
   - Abrir o ficheiro ShooterLand.sln
   - Correr o jogo
   

Caso pretenda jogar uma partida *multiplayer* o servidor deverá estar ligado. Para isso:
- Entrar na pasta shooterland
- Entrar na pasta Servidor
- Entrar na pasta GameServer
- Abrir a linha de comandos e introduzir `dotnet run`


### *Website*
- Entrar na pasta shooterland
- Entrar na pasta Frontend
- Abrir a linha de comandos e introduzir `npm install`
- Entrar na pasta src
- Abrir a linha de comandos e introduzir `ng serve`
- Visitar o *link* http://localhost:4200/homepage

## Documentação
Toda a documentação pode ser consultada em [Documentação](https://gitlab.estg.ipp.pt/lds/shooterland/tree/master/Documenta%C3%A7%C3%A3o).

A documentação da Rest Api pode ser consultada no [Swagger](http://localhost:5000/swagger/index.html)

## Equipa

- André Alves-Responsável pelo desenvolvimento da RestApi, do jogo e de testes ao *Frontend* do *website*
- Fábio Pires-Responsável pelo desenvolviment do *Frontend* do *website* ,do jogo e de testes à Rest Api

    
  
  
  
 


