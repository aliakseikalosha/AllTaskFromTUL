% hudba https://musescore.com/user/10444271/scores/2338126
clc
clear all
close all

Fs = 16000;
bpm = 160;
volume = 0.5;
shiftAct = 0;
shiftTone = 0;

shiftDozvSec = 0;%0.033;
dzvVolume = 0.1;

q = 2^(1/12);
q12 = q^12;

g1  = 391.995;
g2  = g1*q12;
pau = 0;
e1 = 329.628;
e2 = e1*q12;
e2niz = e2/q;

f2 = 698.5;
f2vys = f2*q;

a1	= 440;
alvys = a1*q;
a1niz =	a1/q;
a2 = 440*q12;		

h1 = 493.883;
h1vys = 466.2;

c2 = 523.251;
c3 = c2*q12;

d2 = 587.330;

notesFreq = [e2,e2,pau,e2,pau,c2,e2,g2,pau,g1,pau,c2,pau,g1,pau,e1,pau,a1,pau,h1,pau,h1vys,a1,g1,e2,g2,a2,f2,g2,pau,e2,pau,c2,d2,h1,pau,pau,g2,f2vys,f2,e2niz,pau,e2,pau,a1niz,alvys,c2,pau,a1,c2,e2,pau,g2,f2vys,f2,e2niz,pau,e2,pau,c3,pau,c3,c3,pau,pau,g2,f2vys,f2,e2niz,pau,e2,pau,a1niz,alvys,c2,pau,a1];
notesLenght = [0.125,0.125,0.125,0.125,0.125,0.125,0.25,0.25,0.25,0.25,0.25,0.25,0.125,0.125,0.25,0.25,0.125,0.125,0.125,0.125,0.125,0.125,0.25,0.25,0.25,0.25,0.25,0.125,0.125,0.125,0.125,0.125,0.125,0.125,0.125,0.25,0.25,0.125,0.125,0.125,0.125,0.125,0.125,0.125,0.125,0.25,0.25,0.125,0.125,0.125,0.125,0.25,0.125,0.125,0.125,0.125,0.125,0.125,0.125,0.125,0.125,0.125,0.25,0.25,0.25,0.125,0.125,0.125,0.125,0.125,0.125,0.125,0.125,0.125,0.125,0.125,0.125];
%notesLenght = [1/4,1/4,1/8,1/8,1/16,1/16,1/16,1/16,4/8,1/8,1/8,(2/3)/8,(2/3)/8,(2/3)/8,4/8,1/8,1/8,(2/3)/8,(2/3)/8,(2/3)/8,3/16,1/16,1/2,1/4,1/8,1/16,1/16,1/2,1/8,1/8,1/8,1/16,1/16,1/2,1/8,1/8,1/8,1/16,1/16,1/2,1/4,1/8,1/16,1/16,1/8,1/16,1/16,1/8,1/16,1/16,1/8,1/16,1/16,1/4,1/4,1/8,1/8,1/16,1/16,1/16,1/16,4/8,1/8,1/8,(2/3)/8,(2/3)/8,(2/3)/8,3/4,1/4,1/4,1/2,1/4,3/4,1/4,1/4,1/2,1/4,3/4,1/4,1/4,1/2,1/4,3/4,1/4,1/4,1/2,1/4,1/8,1/16,1/16,1/2,1/4,1/8,1/16,1/16,1/8,1/16,1/16,1/8,1/16,1/16,1/8,1/16,1/16];
%notesFreq = [h1,f1,f1,h1,h1,c2,dis2,e2,f2,f2,f2,f2,g2,a2,h2,h2,h2,h2,a2,g2,a2,g2,f2,f2,e2,e2,f2,g2,f2,e2,d2,d2,e2,f2,e2,d2,c2,c2,dis2,feses2,g2,f2,f1,f1,f1,f1,f1,f1,f1,f1,f1,g1,a1,h1,f1,f1,h1,h1,c2,dis2,e2,f2,f2,f2,f2,g2,a2,h2,d3,c3,a2,f2,g2,h2,ais2,f2,f2,g2,h2  ,ais2,f2,his1,e2,g2,f2,d2,h1,c2,c2,dis2,eis2,gis2,f2,f1,f1,f1,f1,f1,f1,f1,f1,f1,g1,a1];

notesFreq=notesFreq * (2^shiftAct)*(q^shiftTone);

timeTotal=0;
music = [];
%musicTrian = [];
for i=1:size(notesLenght,2)
    noteTime = noteLength(bpm, notesLenght(i));
    t = 0:1/Fs:noteTime-1/Fs;
    music = [music, musicSoundSin(volume,t,notesFreq(i))];
    %musicTrian = [musicTrian, musicSoundSawtooth(volume,t,notesFreq(i))];
    %fprintf('%d : note is %.4f, length is %f sample ammount is %d\n',i,notesLenght(i), noteTime,size(m,2));
end

musicDzv = music + [zeros(1,shiftDozvSec*Fs),music(1:size(music,2)-shiftDozvSec*Fs)] .* dzvVolume;
musicDzv = musicDzv/max(musicDzv)*volume;
%pause
sound(musicDzv,Fs);
%sound(musicTrian);
%audiowrite('zelda.wav',music,Fs);
audiowrite('zeldaDzv.wav',musicDzv,Fs);
%audiowrite('zeldaTrian.wav',musicTrian,Fs);

