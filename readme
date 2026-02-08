## Sobre o projeto

Projeto focado em simular a recepção e decodificação de um protocolo customizado.

Embora desenvolvido em c#, a lógica utilizada espelha desafios comuns no desenvolvimento de firmwares e sistemas embarcado com a leitura de registradores e tratamento de pacotes de dados.

## Conhecimentos aplicados

- **logica non-blocking:** utilizando a função Enviroment.TickCount64 para que o sistema possa lidar com varias tarefas sem travar ou perder dados.

- **Bitwise operations e bitmasking:** usando as operações bit a bit e mascaras para decodificar os dados do protocolo customizado

- **Arquitetura do decodificado baseado em Finite State Machine:** Cada etapa da decodificação se trata de um estado, facilitando a adição de novas verificações e o tratamento dos dados em cada parte do processo;

## Como funciona

- O sistema simula a recepção de dados de um sensor a cada 3 segundos;
- Decodifica as informações do protocolo customizado como HEADER, Status da operação, status de erro e sensibilidade;
- Exibe as informações no terminal e volta para o estado de espera;