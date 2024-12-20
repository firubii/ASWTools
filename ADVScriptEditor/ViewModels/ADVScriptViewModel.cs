﻿using System.Collections.ObjectModel;
using System.Reactive.Linq;
using ADVScriptEditor.ADVScript;

namespace ADVScriptEditor.ViewModels;

public class ADVScriptViewModel : EditorViewModel
{
    private readonly CScriptData _advScriptMem;
    public CParsedScript ParsedScript { get; }
    
    public ADVScriptViewModel()
    {
        _advScriptMem = new CScriptData();
        ParsedScript = new CParsedScript();
    }

    public ADVScriptViewModel(CScriptData inAdvScript)
    {
        _advScriptMem = inAdvScript;
        ParsedScript = new CParsedScript(_advScriptMem);
    }

    public ADVScriptViewModel(CParsedScript inParsedScript)
    {
        _advScriptMem = new CScriptData();
        ParsedScript = inParsedScript;
    }

    public void AddCommand(int index)
    {
        ParsedScript.Commands.Insert(index, new SParsedCommand());
    }

    public void DeleteCommand(SParsedCommand command)
    {
        if (ParsedScript.Commands.Count > 1) ParsedScript.Commands.Remove(command);
    }

    public override void PrepareSave()
    {
        var newScript = ParsedScript.Compile();

        _advScriptMem.m_ScriptHeader = newScript.m_ScriptHeader;
        _advScriptMem.m_ParamHeader = newScript.m_ParamHeader;
        _advScriptMem.m_CommandList = newScript.m_CommandList;
        _advScriptMem.m_StringHeader = newScript.m_StringHeader;
        _advScriptMem.m_StringBuffer = newScript.m_StringBuffer;
    }
}