		glBegin(GL_TRIANGLE_STRIP);
		float r1 = 100;
		float r2 = 200;
		glm::vec3 center(globals.width / 2.0f, globals.height / 2.0f, 0.0f);
		
		for (float phi = 0.0f; phi < 360.0f; phi+=0.001f)
		{
			glm::vec2 unit(cos(phi), sin(phi));
			glm::vec3 p1(r1 * unit, 0.0);
			glm::vec3 p2(r2 * unit, 0.0);
			glVertex3fv(glm::value_ptr(p1 + center));
			glVertex3fv(glm::value_ptr(p2 + center));
		}
		glEnd();

