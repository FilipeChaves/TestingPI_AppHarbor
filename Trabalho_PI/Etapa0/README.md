## Relatório da Etapa 1:

 * Este relatório vai ser apresentado através de ficheiros README.md, para que tenham uma boa visualização no GitHub.
 * Em cada pasta encontra-se um destes ficheiros, explicando melhor como é que o nosso grupo pensou e o porquê do que fizemos ao longo desta etapa.

 
## Significado dos vários URI's escolhidos pelo grupo:
 * /boards
  * POST de um board ou GET de todos os boards existentes.
 * /cards/{cid}
  * GET do card associado ao id "cid" (não interessa a que lista ou board esta associado);
  * Colocar o card respectivo em arquivo.
 * /boards/{bid}/lists
  * POST de uma lista ou GET de todas as listas associadas ao board identificado pelo id "bid".
 * /boards/{bid}/lists/{lid}/cards
  * POST de um card ou GET de todos os cards associados à lista identificada pelo id "lid" do Board com id "bid".
 * /boards/{bid}/lists/{lid}/cards/{cid}
  * GET do card associado ao id "cid", na lista "lid" e no board "bid";
  * Modificação do idx associado à lista (posição em relação aos outros cards) ou mesmo modificação da lista ao qual estava associado.