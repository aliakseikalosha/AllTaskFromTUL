clc;clear all; close all; echo off;

key = 'matlab';
text = 'AHOJJAJSEMTVUJSUPERPOMOCNIK';

[~ ,cisla]= sort(double(key));
M = vec2mat(text, length(key));
reshape((M(:,cisla)),1,[])
