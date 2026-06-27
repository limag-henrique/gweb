# Clone: G Web Development Software IDE

Este é um aplicativo local, construído em tecnologias web (HTML, CSS e Javascript) para replicar a interface e as principais interatividades da ferramenta oficial de desenvolvimento **G Web Development Software** (base do NI WebVIs), de forma estritamente offline e sem dependência de plataformas na nuvem como o SystemLink.

## Como Iniciar e Abrir o Aplicativo

Como esse projeto foi desenvolvido para funcionar localmente num ambiente Windows e parecer 100% com um software nativo (App de PC), você não precisa configurar servidores complicados.

**Passo a passo:**
1. Navegue até o diretório onde os arquivos estão salvos (esta pasta `GWeb_Clone`).
2. Localize o arquivo executável chamado **`Iniciar_App.bat`**.
3. **Dê um duplo-clique no arquivo `Iniciar_App.bat`**.

> *O que isso faz?* Este script invocará automaticamente o navegador nativo (Microsoft Edge) usando o "Modo App". A interface se abrirá em uma janela própria de software, sem exibição barra de endereço ou abas de navegação comuns, entregando a você uma experiência limpa e focada.

## Principais Funcionalidades Implementadas

O aplicativo simula operações complexas guiadas por dados (Dataflow) em um ambiente drag-and-drop:

- **Alternância de Visão**: Utilize o atalho **Ctrl+E** (ou as abas superiores "Painel" e "Diagrama") para alternar entre o design da Interface do Usuário (Painel GUI) e o código Fonte Visual (Diagrama G de Dataflow).
- **Paleta Responsiva Inteligente**: Na barra lateral esquerda (Aba "Paleta"), as categorias de componentes são carregadas dependendo da aba que você está visualizando:
    - No **Painel**: Arraste numéricos, botões, tanques de medição, botões LEDs e gráficos diretamente para o canvas.
    - No **Diagrama**: Encontre as estruturas lógicas (*While Loop*, *For Loop*, *Case Structure*) e nós matemáticos puros para diagramar sua inteligência.
- **Explorador de Projetos**: Na barra lateral esquerda, a aba **"Projeto"** permite você explorar a estrutura física simulada do projeto local (`WebApp.gcomp`, etc.).
- **Inspetor de Propriedades**: Clique em qualquer elemento que você soltar no painel central; a barra direita se ajustará ativamente para mostrar suas Propriedades exclusivas e a opção para removê-lo facilmente.
- **Compilador Embarcado (Offline Build)**: Ao clicar no botão azul **"Build"** no topo da tela, no modo "Painel", o javascript interno reunirá todos os elementos visuais aplicados, gerará um código HTML local da sua aplicação e fará um download direto na sua máquina com o projeto compilado — emulando perfeitamente os comandos `gwebcli.exe build-application` mas isolado da internet.

## Estrutura dos Arquivos

Caso deseje modificar a ferramenta de alguma forma, o frontend puro consiste da seguinte estrutura:
- `index.html`: A disposição base do Software, Menus e Botões.
- `style.css`: Estilização (layout acordeão, desenhos de LEDS, tanques, botões).
- `script.js`: O cérebro que gerencia Drag and Drop, atalhos de teclado (Ctrl+E, S, R) e o "Offline Builder".
- `Iniciar_App.bat`: O ativador instantâneo do Software para Windows.
