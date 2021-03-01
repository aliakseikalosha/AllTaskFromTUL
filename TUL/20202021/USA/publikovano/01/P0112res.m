clear all
clc

% jedná se o permutace s opakováním, protože z osudí vyberu všechna èísla,
% kde se nìkterá z nich opakují
jednicek=3;
dvojek=4;
trojek=5;

celkem=jednicek+dvojek+trojek;
%podíl z toho dùvodu, že nejsme schopni rozlišit zda máme èíslo 11 nebo v
%opaèném ciferném poøadí 11
permutaci=factorial(celkem)/(factorial(jednicek)*factorial(dvojek)*factorial(trojek))
