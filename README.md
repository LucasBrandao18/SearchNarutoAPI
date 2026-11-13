# SearchNarutoAPI

Aplicação Blazor WebAssembly em .NET 10 para explorar os personagens de Naruto. O catálogo e as fichas são carregados diretamente da Dattebayo API pelo navegador.

## O que o exercício pede

| Item da lista 26 | Implementação |
| --- | --- |
| a) Consumir a Dattebayo API em Blazor WebAssembly com .NET 10 ou superior | Projeto `SearchNarutoAPI.csproj` com destino `net10.0`. |
| b) Listar ou buscar personagens | A página inicial lista 12 personagens por vez. O campo de busca filtra o catálogo por nome. |
| c) Exibir os dados retornados | A ficha mostra nome, imagem, clã, afiliação, patentes ninja por período e a lista completa de jutsus disponíveis. |
| d) e e) Publicação e entrega | A criação do repositório, a publicação no GitHub e o envio do link ao Google Classroom ficam para entrega manual. |

## Como executar

Instale o SDK do .NET 10. É preciso ter acesso à internet para consultar a API e carregar as imagens. Na primeira execução, o SDK também precisa obter os pacotes do projeto se eles ainda não estiverem no cache.

No PowerShell, execute estes comandos:

```powershell
cd "C:\Users\lucas\OneDrive\Documentos\vs code uni\homework-Dattebayo API"
dotnet run
```

Abra [http://localhost:5055](http://localhost:5055) ou o endereço exibido pelo terminal. Para encerrar, pressione `Ctrl+C`. Para apenas compilar, execute `dotnet build` na mesma pasta.

## Como usar

1. Abra o catálogo. Use **Próxima** e **Anterior** para percorrer as páginas.
2. Digite parte de um nome, como `Sakura`, e pressione **Enter** ou clique em **Buscar**. Uma busca em branco volta à primeira página do catálogo completo.
3. Clique no cartão de um personagem para abrir sua ficha. **Voltar aos resultados** recupera a lista e a página consultadas.

## Como o código funciona

- **`Program.cs`** registra o `HttpClient` com a URL base `https://dattebayo-api.onrender.com/` e disponibiliza o serviço de consulta para os componentes.
- **`Services/DattebayoClient.cs`** faz duas consultas assíncronas. A listagem usa `GET /characters?page=1&limit=12`. Quando há um nome, o serviço acrescenta o parâmetro `name` com o texto codificado para URL. A ficha usa `GET /characters/{id}` com o identificador do cartão selecionado.
- **`Models/Character.cs`** representa a resposta paginada (`characters` e `total`) e o personagem individual. Clã e afiliação aceitam texto ou lista. O dicionário `rank.ninjaRank` preserva a correspondência entre cada período da história e sua patente.
- **`Pages/Home.razor`** consulta a primeira página ao iniciar, reinicia a paginação ao buscar e usa o total retornado para habilitar os botões de navegação. Exibe mensagens para carregamento, busca vazia de resultados, falha de rede, tempo esgotado ou resposta inválida. O botão **Tentar novamente** repete a última consulta que falhou.
- **`Components/CharacterSheet.razor`** exibe os campos da ficha e identifica dados que a API omitiu. **`Components/Portrait.razor`** usa as URLs de `images` na ordem recebida. Se o servidor bloquear o carregamento direto, solicita ao serviço de miniaturas wsrv.nl uma versão da mesma URL com largura de 600 pixels. Se essa tentativa também falhar, passa à próxima URL da lista. Se nenhuma carregar, mostra **Imagem indisponível**.
- **`wwwroot/css/app.css`** adapta o catálogo e as fichas para telas pequenas e grandes.

Consulte a [documentação da Dattebayo API](https://api-dattebayo.vercel.app/docs) e um [exemplo de resposta da lista](https://dattebayo-api.onrender.com/characters?limit=1).
