function varargout = testc10(varargin)
% TESTC10 MATLAB code for testc10.fig
%      TESTC10, by itself, creates a new TESTC10 or raises the existing
%      singleton*.
%
%      H = TESTC10 returns the handle to a new TESTC10 or the handle to
%      the existing singleton*.
%
%      TESTC10('CALLBACK',hObject,eventData,handles,...) calls the local
%      function named CALLBACK in TESTC10.M with the given input arguments.
%
%      TESTC10('Property','Value',...) creates a new TESTC10 or raises the
%      existing singleton*.  Starting from the left, property value pairs are
%      applied to the GUI before testc10_OpeningFcn gets called.  An
%      unrecognized property name or invalid value makes property application
%      stop.  All inputs are passed to testc10_OpeningFcn via varargin.
%
%      *See GUI Options on GUIDE's Tools menu.  Choose "GUI allows only one
%      instance to run (singleton)".
%
% See also: GUIDE, GUIDATA, GUIHANDLES

% Edit the above text to modify the response to help testc10

% Last Modified by GUIDE v2.5 13-Jan-2020 16:29:34

% Begin initialization code - DO NOT EDIT
gui_Singleton = 1;
gui_State = struct('gui_Name',       mfilename, ...
                   'gui_Singleton',  gui_Singleton, ...
                   'gui_OpeningFcn', @testc10_OpeningFcn, ...
                   'gui_OutputFcn',  @testc10_OutputFcn, ...
                   'gui_LayoutFcn',  [] , ...
                   'gui_Callback',   []);
if nargin && ischar(varargin{1})
    gui_State.gui_Callback = str2func(varargin{1});
end

if nargout
    [varargout{1:nargout}] = gui_mainfcn(gui_State, varargin{:});
else
    gui_mainfcn(gui_State, varargin{:});
end
% End initialization code - DO NOT EDIT


% --- Executes just before testc10 is made visible.
function testc10_OpeningFcn(hObject, eventdata, handles, varargin)
% This function has no output args, see OutputFcn.
% hObject    handle to figure
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    structure with handles and user data (see GUIDATA)
% varargin   command line arguments to testc10 (see VARARGIN)

% Choose default command line output for testc10
handles.output = hObject;

% Update handles structure
guidata(hObject, handles);

% UIWAIT makes testc10 wait for user response (see UIRESUME)
% uiwait(handles.figure1);


% --- Outputs from this function are returned to the command line.
function varargout = testc10_OutputFcn(hObject, eventdata, handles) 
% varargout  cell array for returning output args (see VARARGOUT);
% hObject    handle to figure
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    structure with handles and user data (see GUIDATA)

% Get default command line output from handles structure
varargout{1} = handles.output;


% --- Executes on button press in pushbutton1.
function pushbutton1_Callback(hObject, eventdata, handles)
% hObject    handle to pushbutton1 (see GCBO)
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    structure with handles and user data (see GUIDATA) 
xx = str2num(handles.edit1.String);
x = -xx:0.1:xx;
handles.text2.Visible = true;
if handles.radiobutton2.Value == 1
    plot(x,sin(x))
    f = 'sinus';
else
    plot(x,cos(x))
    f = 'cosinus'
end

handles.text2.String = [f ' vyploceno od -'  num2str(xx) ' do ' num2str(xx)];



function edit1_Callback(hObject, eventdata, handles)
% hObject    handle to edit1 (see GCBO)
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    structure with handles and user data (see GUIDATA)

% Hints: get(hObject,'String') returns contents of edit1 as text
%        str2double(get(hObject,'String')) returns contents of edit1 as a double


% --- Executes during object creation, after setting all properties.
function edit1_CreateFcn(hObject, eventdata, handles)
% hObject    handle to edit1 (see GCBO)
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    empty - handles not created until after all CreateFcns called

% Hint: edit controls usually have a white background on Windows.
%       See ISPC and COMPUTER.
if ispc && isequal(get(hObject,'BackgroundColor'), get(0,'defaultUicontrolBackgroundColor'))
    set(hObject,'BackgroundColor','white');
end
