using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SS.View;

public class TemplateController : Controller
{
    public const string TEMPLATE_SCENE_NAME = "Template";

    public override string SceneName()
    {
        return TEMPLATE_SCENE_NAME;
    }
}