
typedef struct s_globals {
	GLFWwindow *window;
	cv::VideoCapture capture;
	int height;
	int width;
	double app_start_time;

	// mesh
	mesh height_map;
} s_globals;

// the only global variable - encapsulates all
s_globals globals = { nullptr, };

typedef struct  Avatar {                            // player (viewer) info
	float       posX;
	float       posY;
	float       angle;
} Avatar;

Avatar  avatar = { 10.0, 0.0, 0.0 };

//---------------------------------------------------------------------
// Mouse pressed?
//---------------------------------------------------------------------
void mouse_button_callback(GLFWwindow* window, int button, int action, int mods)
{
	if (button == GLFW_MOUSE_BUTTON_LEFT) {
		if (action == GLFW_PRESS) {
      //action
		}
	}
	if (button == GLFW_MOUSE_BUTTON_RIGHT) {
		if (action == GLFW_PRESS) {
			//action
		}
	}
}

//---------------------------------------------------------------------
// Mose moved?
//---------------------------------------------------------------------
static void cursor_position_callback(GLFWwindow* window, double xpos, double ypos)
{
	static int first = 1;
	static int old_x;
	if (first) {
		old_x = xpos;
		first = 0;
	}
	else {
		avatar.angle = -xpos + old_x;
	}
}

static void key_callback(GLFWwindow* window, int key, int scancode, int action, int mods)
{
	if ((action == GLFW_PRESS) || (action == GLFW_REPEAT))
	{
		switch (key)
		{
		case GLFW_KEY_ESCAPE:
			glfwSetWindowShouldClose(window, GLFW_TRUE);
			break;
		case GLFW_KEY_W:
			avatarMoveForward(&avatar);
			break;
		case GLFW_KEY_S:
			avatarMoveBackward(&avatar);
			break;
		case GLFW_KEY_A:
			avatarMoveLeft(&avatar);
			break;
		case GLFW_KEY_D:
			avatarMoveRight(&avatar);
			break;
		default:
			break;
		}
	}
}
