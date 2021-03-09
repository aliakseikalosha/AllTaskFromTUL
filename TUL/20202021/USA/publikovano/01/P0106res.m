clear all
clc

%kombinatorické pravidlo souèinu
%pøedposlední cifru lze vybrat z desíti èísel
predposledni=10;
%poslední cifru lze vybrat z devíti èísel - jedno je již obsazeno
posledni=9;
pocet_moznosti=predposledni*posledni
%jedno úspìšné èíslo/poèet možností
pravdepodobnost=1/pocet_moznosti