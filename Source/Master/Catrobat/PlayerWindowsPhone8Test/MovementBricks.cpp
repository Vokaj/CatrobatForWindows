#include "pch.h"
#include "CppUnitTest.h"
#include "Object.h"
#include "StartScript.h"
#include "ChangeXByBrick.h"
#include "ChangeYByBrick.h"
#include "GlideToBrick.h"
#include "PlaceAtBrick.h"
#include "PointToBrick.h"
#include "SetXBrick.h"
#include "SetYBrick.h"
#include "TurnLeftBrick.h"
#include "TurnRightBrick.h"
#include "FormulaTree.h"
#include "TestHelper.h"

using namespace Microsoft::VisualStudio::CppUnitTestFramework;

namespace PlayerWindowsPhone8Test
{
    TEST_CLASS(MovementBricks)
    {
    public:
        TEST_METHOD(MovementBricks_ChangeXByBrick)
        {
			Object* object = new Object("");
			Script* script = new StartScript("", object);
			FormulaTree* tree = new FormulaTree("NUMBER", "42");
			Brick* brick = new ChangeXByBrick("", tree, script);
			
			float old_x = 0;
			float old_y = 0;
			object->SetPosition(old_x, old_y);

			brick->Execute();

			float x = 0;
			float y = 0;
			object->GetPosition(x, y);

			Assert::AreEqual(x, old_x + 42);

			brick->Execute();

			object->GetPosition(x, y);

			Assert::AreEqual(x, old_x + 84);
        }

		TEST_METHOD(MovementBricks_ChangeXByBrick_Negative)
        {
			Object* object = new Object("");
			Script* script = new StartScript("", object);
			FormulaTree* tree = new FormulaTree("NUMBER", "-42");
			Brick* brick = new ChangeXByBrick("", tree, script);
			
			float old_x = 0;
			float old_y = 0;
			object->SetPosition(old_x, old_y);

			brick->Execute();

			float x = 0;
			float y = 0;
			object->GetPosition(x, y);

			Assert::AreEqual(x, old_x - 42);

			brick->Execute();

			object->GetPosition(x, y);

			Assert::AreEqual(x, old_x - 84);
        }

		TEST_METHOD(MovementBricks_ChangeXByBrick_zero)
        {
			Object* object = new Object("");
			Script* script = new StartScript("", object);
			FormulaTree* tree = new FormulaTree("NUMBER", "0");
			Brick* brick = new ChangeXByBrick("", tree, script);
			
			float old_x = 0;
			float old_y = 0;
			object->SetPosition(old_x, old_y);

			brick->Execute();

			float x = 0;
			float y = 0;
			object->GetPosition(x, y);

			Assert::AreEqual(x, old_x);

			brick->Execute();

			object->GetPosition(x, y);

			Assert::AreEqual(x, old_x);
        }

		TEST_METHOD(MovementBricks_ChangeYByBrick)
        {
			Object* object = new Object("");
			Script* script = new StartScript("", object);
			FormulaTree* tree = new FormulaTree("NUMBER", "42");
			Brick* brick = new ChangeYByBrick("", tree, script);
			
			float old_x = 0;
			float old_y = 0;
			object->SetPosition(old_x, old_y);

			brick->Execute();

			float x = 0;
			float y = 0;
			object->GetPosition(x, y);

			Assert::AreEqual(y, old_y + 42);

			brick->Execute();

			object->GetPosition(x, y);

			Assert::AreEqual(y, old_y + 84);
        }

		TEST_METHOD(MovementBricks_ChangeYByBrick_Negative)
        {
			Object* object = new Object("");
			Script* script = new StartScript("", object);
			FormulaTree* tree = new FormulaTree("NUMBER", "-42");
			Brick* brick = new ChangeYByBrick("", tree, script);
			
			float old_x = 0;
			float old_y = 0;
			object->SetPosition(old_x, old_y);

			brick->Execute();

			float x = 0;
			float y = 0;
			object->GetPosition(x, y);

			Assert::AreEqual(y, old_y - 42);

			brick->Execute();

			object->GetPosition(x, y);

			Assert::AreEqual(y, old_y - 84);
        }

		TEST_METHOD(MovementBricks_ChangeYByBrick_Zero)
        {
			Object* object = new Object("");
			Script* script = new StartScript("", object);
			FormulaTree* tree = new FormulaTree("NUMBER", "0");
			Brick* brick = new ChangeYByBrick("", tree, script);
			
			float old_x = 0;
			float old_y = 0;
			object->SetPosition(old_x, old_y);

			brick->Execute();

			float x = 0;
			float y = 0;
			object->GetPosition(x, y);

			Assert::AreEqual(y, old_y);

			brick->Execute();

			object->GetPosition(x, y);

			Assert::AreEqual(y, old_y);
        }

		TEST_METHOD(MovementBricks_GlideToBrick_Variance1)
        {
			Object* object = new Object("");
			Script* script = new StartScript("", object);
			FormulaTree* tree1 = new FormulaTree("NUMBER", "42");
			FormulaTree* tree2 = new FormulaTree("NUMBER", "23");
			FormulaTree* tree3 = new FormulaTree("NUMBER", "3000");
			Brick* brick = new GlideToBrick("", tree1, tree2, tree3, script);
			object->SetPosition(0, 0);

			brick->Execute();

			float x = 0;
			float y = 0;
			object->GetPosition(x, y);

			Assert::IsTrue(TestHelper::isEqual(x, 42.0f));
			Assert::IsTrue(TestHelper::isEqual(y, 23.0f));

			// TODO: Measure Time.
        }

		TEST_METHOD(MovementBricks_GlideToBrick_Variance2)
        {
			Object* object = new Object("");
			Script* script = new StartScript("", object);
			FormulaTree* tree1 = new FormulaTree("NUMBER", "123");
			FormulaTree* tree2 = new FormulaTree("NUMBER", "456");
			FormulaTree* tree3 = new FormulaTree("NUMBER", "5000");
			Brick* brick = new GlideToBrick("", tree1, tree2, tree3, script);
			object->SetPosition(0, 0);

			brick->Execute();

			float x = 0;
			float y = 0;
			object->GetPosition(x, y);

			Assert::IsTrue(TestHelper::isEqual(x, 123.0f));
			Assert::IsTrue(TestHelper::isEqual(y, 456.0f));

			// TODO: Measure Time.
        }

		TEST_METHOD(MovementBricks_PlaceAtBrick_Variance1)
        {
			Object* object = new Object("");
			Script* script = new StartScript("", object);
			FormulaTree* tree1 = new FormulaTree("NUMBER", "23");
			FormulaTree* tree2 = new FormulaTree("NUMBER", "32");
			Brick* brick = new PlaceAtBrick("", tree1, tree2, script);

			brick->Execute();

			float x = 0;
			float y = 0;
			object->GetPosition(x, y);

			Assert::AreEqual(x, 23.0f);
			Assert::AreEqual(y, 32.0f);
        }

		TEST_METHOD(MovementBricks_PlaceAtBrick_Variance2)
        {
			Object* object = new Object("");
			Script* script = new StartScript("", object);
			FormulaTree* tree1 = new FormulaTree("NUMBER", "23");
			FormulaTree* tree2 = new FormulaTree("NUMBER", "32");
			Brick* brick = new PlaceAtBrick("", tree1, tree2, script);

			brick->Execute();

			float x = 0;
			float y = 0;
			object->GetPosition(x, y);

			Assert::AreEqual(x, 23.0f);
			Assert::AreEqual(y, 32.0f);
        }

		TEST_METHOD(MovementBricks_PointToBrick_Variance1)
        {
			Object* object = new Object("");
			Script* script = new StartScript("", object);
			FormulaTree* tree = new FormulaTree("NUMBER", "42");
			Brick* brick = new PointToBrick("", tree, script);
			object->SetRotation(0.0f);

			brick->Execute();

			float r = object->GetRotation();

			Assert::AreEqual(r, 42.0f);

			brick->Execute();

			r = object->GetRotation();

			Assert::AreEqual(r, 42.0f);
        }

		TEST_METHOD(MovementBricks_PointToBrick_Variance2)
        {
			Object* object = new Object("");
			Script* script = new StartScript("", object);
			FormulaTree* tree = new FormulaTree("NUMBER", "23");
			Brick* brick = new PointToBrick("", tree, script);
			object->SetRotation(0.0f);

			brick->Execute();

			float r = object->GetRotation();

			Assert::AreEqual(r, 23.0f);

			brick->Execute();

			r = object->GetRotation();

			Assert::AreEqual(r, 23.0f);
        }

		TEST_METHOD(MovementBricks_SetXBrick)
        {
			Object* object = new Object("");
			Script* script = new StartScript("", object);
			FormulaTree* tree = new FormulaTree("NUMBER", "42");
			Brick* brick = new SetXBrick("", tree, script);

			brick->Execute();

			float x = 0;
			float y = 0;
			object->GetPosition(x, y);

			Assert::AreEqual(x, 42.0f);
        }

		TEST_METHOD(MovementBricks_SetXBrick_Negative)
        {
			Object* object = new Object("");
			Script* script = new StartScript("", object);
			FormulaTree* tree = new FormulaTree("NUMBER", "-42");
			Brick* brick = new SetXBrick("", tree, script);

			brick->Execute();

			float x = 0;
			float y = 0;
			object->GetPosition(x, y);

			Assert::AreEqual(x, -42.0f);
        }

		TEST_METHOD(MovementBricks_SetXBrick_Zero)
        {
			Object* object = new Object("");
			Script* script = new StartScript("", object);
			FormulaTree* tree = new FormulaTree("NUMBER", "0");
			Brick* brick = new SetXBrick("", tree, script);

			brick->Execute();

			float x = 0;
			float y = 0;
			object->GetPosition(x, y);

			Assert::AreEqual(x, 0.0f);
        }

		TEST_METHOD(MovementBricks_SetYBrick)
        {
			Object* object = new Object("");
			Script* script = new StartScript("", object);
			FormulaTree* tree = new FormulaTree("NUMBER", "42");
			Brick* brick = new SetYBrick("", tree, script);

			brick->Execute();

			float x = 0;
			float y = 0;
			object->GetPosition(x, y);

			Assert::AreEqual(y, 42.0f);
        }

		TEST_METHOD(MovementBricks_SetYBrick_Negative)
        {
			Object* object = new Object("");
			Script* script = new StartScript("", object);
			FormulaTree* tree = new FormulaTree("NUMBER", "-42");
			Brick* brick = new SetYBrick("", tree, script);

			brick->Execute();

			float x = 0;
			float y = 0;
			object->GetPosition(x, y);

			Assert::AreEqual(y, -42.0f);
        }

		TEST_METHOD(MovementBricks_SetYBrick_Zero)
        {
			Object* object = new Object("");
			Script* script = new StartScript("", object);
			FormulaTree* tree = new FormulaTree("NUMBER", "0");
			Brick* brick = new SetYBrick("", tree, script);

			brick->Execute();

			float x = 0;
			float y = 0;
			object->GetPosition(x, y);

			Assert::AreEqual(y, 0.0f);
        }

		TEST_METHOD(MovementBricks_TurnLeftBrick)
        {
			Object* object = new Object("");
			Script* script = new StartScript("", object);
			FormulaTree* tree = new FormulaTree("NUMBER", "42");
			Brick* brick = new TurnLeftBrick("", tree, script);
			object->SetRotation(0.0f);

			brick->Execute();

			float r = object->GetRotation();

			Assert::AreEqual(r, -42.0f);

			brick->Execute();

			r = object->GetRotation();

			Assert::AreEqual(r, -84.0f);
        }

		TEST_METHOD(MovementBricks_TurnLeftBrick_Zero)
        {
			Object* object = new Object("");
			Script* script = new StartScript("", object);
			FormulaTree* tree = new FormulaTree("NUMBER", "0");
			Brick* brick = new TurnLeftBrick("", tree, script);
			object->SetRotation(23.0f);

			brick->Execute();

			float r = object->GetRotation();

			Assert::AreEqual(r, 23.0f);

			brick->Execute();

			r = object->GetRotation();

			Assert::AreEqual(r, 23.0f);
        }

		TEST_METHOD(MovementBricks_TurnLeftBrick_Negative)
        {
			Object* object = new Object("");
			Script* script = new StartScript("", object);
			FormulaTree* tree = new FormulaTree("NUMBER", "-42");
			Brick* brick = new TurnLeftBrick("", tree, script);
			object->SetRotation(0.0f);

			brick->Execute();

			float r = object->GetRotation();

			Assert::AreEqual(r, 42.0f);

			brick->Execute();

			r = object->GetRotation();

			Assert::AreEqual(r, 84.0f);
        }

		TEST_METHOD(MovementBricks_TurnRightBrick)
        {
			Object* object = new Object("");
			Script* script = new StartScript("", object);
			FormulaTree* tree = new FormulaTree("NUMBER", "42");
			Brick* brick = new TurnRightBrick("", tree, script);
			object->SetRotation(0.0f);

			brick->Execute();

			float r = object->GetRotation();

			Assert::AreEqual(r, 42.0f);

			brick->Execute();

			r = object->GetRotation();

			Assert::AreEqual(r, 84.0f);
        }

		TEST_METHOD(MovementBricks_TurnRightBrick_Zero)
        {
			Object* object = new Object("");
			Script* script = new StartScript("", object);
			FormulaTree* tree = new FormulaTree("NUMBER", "0");
			Brick* brick = new TurnRightBrick("", tree, script);
			object->SetRotation(23.0f);

			brick->Execute();

			float r = object->GetRotation();

			Assert::AreEqual(r, 23.0f);

			brick->Execute();

			r = object->GetRotation();

			Assert::AreEqual(r, 23.0f);
        }

		TEST_METHOD(MovementBricks_TurnRightBrick_Negative)
        {
			Object* object = new Object("");
			Script* script = new StartScript("", object);
			FormulaTree* tree = new FormulaTree("NUMBER", "-42");
			Brick* brick = new TurnRightBrick("", tree, script);
			object->SetRotation(0.0f);

			brick->Execute();

			float r = object->GetRotation();

			Assert::AreEqual(r, -42.0f);

			brick->Execute();

			r = object->GetRotation();

			Assert::AreEqual(r, -84.0f);
        }
    };
}