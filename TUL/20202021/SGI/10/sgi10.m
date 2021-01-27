clear all;
close all;
clc;


im1 = rgb2gray(imread('ruze.bmp', 'bmp'));
im2 = rgb2gray(imread('obr.bmp', 'bmp'));
%im2 = ones(3,3)/3; % průměrovací filtr
%im2 = ones(9,9)/9; % průměrovací filtr
%im2 = [1 -1 1; -1 -0 -1; 1 -1 1]; % HP filter
%im2 = [0 -1/4 0; -1/4 2 -1/4; 0 -1/4 0]; % HP filter
%im2 = [-1/9 -1/9 -1/9; -1/9 8/9 -1/9; -1/9 -1/9 -1/9]; % HP filter

X = conv2(double(im1), double(im2));
subplot(1,4,1)
imshow(imread('ruze.bmp', 'bmp'))
subplot(1,4,2)
imshow(im1)
subplot(1,4,3)
imshow(im2)
subplot(1,4,4)
imshow(X/max(max(X)))

