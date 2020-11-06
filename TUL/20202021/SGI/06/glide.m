% MELODY.M - vygeneruje 12 tonu stupnice
%-------------------------------------------
clear all;
close all;
clc;
% zvol vzorkovaci frekvenci
Fs = 16000;


%vygeneruj casovou osu 
t=0:1/Fs:0.2;

tend=1;
t1 = 0.2*Fs;
q = 2^(1/12);
f = 440;
for k = 0:60 
   f1 = f * q^k;
   ton = 0.3*sin(2*pi*f1*t(1:t1)) + 0.3*sin(2*q^7*pi*f1*t(1:t1)) + 0.3*sin(2*q^4*pi*f1*t(1:t1));

   tstart=tend;
   tend=tstart+t1-1;
   mel(tstart:tend)=ton;
   echo off
end
% prehrajeme cele pole 
sound(mel, Fs)

% zobrazime spektrum melodie
spectrogram(mel, hamming(256), 128, 256, Fs, 'yaxis');
title(['Fs = ', num2str(Fs), ' Hz']);



