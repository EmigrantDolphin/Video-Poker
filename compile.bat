echo off
setlocal EnableDelayedExpansion

set files=

for %%f in (*.cs) do set files=!files! %%f

csc %files% && cls && main