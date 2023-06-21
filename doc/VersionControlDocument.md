# Version Control Document

## Contents

1. Basic Workflow  
1.1. Clone  
1.2. Pull  
1.3. Add  
1.4 Commit  
1.5 Push  
1.6 Merge
<br><br>
2. Branches  
2.1 Purpose  
2.2 Creating a Branch  
2.3 Switching Branches  
2.4 Merging Branches
<br> <br>
3. Guidelines  
3.1 Important Rules  
3.2 Naming Convention  
3.2.1 Commit Messages  
3.2.2 Branches
<br><br>
4. Common Issues (and how to fix them)
5. Glossary
<br><br><br>
## 1. Basic Workflow
### 1.1 Clone
Before you can use an repository you must first clone it.  
There are two ways to clone an repository using Github Desktop:  
#### Clone via App
<img width="188" height="170" alt="image" src="https://github.com/bene-labs/S4G_GameJam1_2023/assets/62158116/2be01574-2d56-49a7-be3d-98fb942130aa" align=right> <img width="188" height="170" alt="image" src="https://github.com/bene-labs/S4G_GameJam1_2023/assets/62158116/3d5e7cb9-d762-4940-8382-110597b57879" align=right>
You can clone an Repo via Github Desktop directly. To do so head to the *File* Header and press *Clone Repository*.
On the resulting screen you can choose from any repository that you created or a an member of. 
Once you've chosen an repository you can choose where it should be saved on your computer under the *Local Path* Header and then press *clone* to clone it.
The repository can now be used a soon as it is finished downloading.
#### Clone from Web
You can also clone a repository from the Github Website. To do so press the *open with github desktop* button under the *Code* dropdown button.  
<div>
<img width="272" alt="image" src="https://github.com/bene-labs/S4G_GameJam1_2023/assets/62158116/24f000f4-d6be-4ab9-9e2b-c38f9f7f1a65" align=middle > 
<img width="263" alt="image" src="https://github.com/bene-labs/S4G_GameJam1_2023/assets/62158116/1e89e197-c947-4213-9159-572d825d3c64" align=middle > 
<img width="354" alt="image" src="https://github.com/bene-labs/S4G_GameJam1_2023/assets/62158116/2b40350d-8701-4274-9155-c74710512b15" align=middle > 
</div>
<br>

### 1.2 Pull
<img width="118" alt="image" src="https://github.com/bene-labs/S4G_GameJam1_2023/assets/62158116/189d3542-ffed-4291-9c64-dfad8577d889" align=right>

In order to keep your Repository up-to-date you must regularly pull new commits.  
To check if any new Versions are available you must first refresh your local repository by pressing the *fetch* button. 
If any new Commits are available the button will be replaced by a *pull* button with the the number indicating the amount of availalbe commits. Press it to pull the incoming changes.


#### Pull Error
<img width="800" alt="image" src="https://github.com/bene-labs/S4G_GameJam1_2023/assets/62158116/51cfc447-30b1-4d63-b21f-6c2e9d6b3c98" align=middle>
<br>

If you try to pull changes to a file that you have local uncommitted changes to, a Pull Error will appear. To fix it either close the error message and commit all changes to the offending files first or press the "stash changes and continue" button.
In case you don't want to keep the changes to an offending file you can also fix the error by right-clicking it in the change list and pressing *discard changes*.  
If you press the *stash and continue* button all uncommitted changes will be saved to a "stash" backup and will then be deleted. 
You can restore them afterwards by pressing on the *stashed changes* banner that will now appear at the bottom of your change list.  
**Watch out:** you can only have one set of changes stashed at the same time. 
If you already have stashed changes that were not yet restored stashing something again will permanently delete the previous stashed changes.

### 1.3 Add
<img width="242" alt="image" src="https://github.com/bene-labs/S4G_GameJam1_2023/assets/62158116/a87a6b79-d6a5-4f9f-a89a-d6aa01324a82" align=right>
Each modified file that you want to upload must first be added before you can commit and push it.|
To add a file in Github Desktop simply check the box next to it in the change list.
You can also add/unadd all files at once by pressing the topmost checkbox.

### 1.4 Commit
<img width="250" alt="image" src="https://github.com/bene-labs/S4G_GameJam1_2023/assets/62158116/46482b95-0006-46bb-a01d-4f5a65c88e95" align=right>

Commits are used to categorize a set of changes as a new version of the repository. Before you can push your added files you must first commit them.
To commit changes you must write a Commit message and then press the *Commit to (current branch)* button.  
**Please note:** commit messages must describe the uploaded changes well and shouldn't be to long. You don't have to commit all changes at once, frequent commits containig smaller changes are better.

### 1.5 Push
<img width="110" alt="image" src="https://github.com/bene-labs/S4G_GameJam1_2023/assets/62158116/2b749df7-5c8d-4008-a387-518e2075194e" align = right>


Pushing means uploading all local commits. Everything you haven't pushed yet will not be stored online.
Once you commit changes the *fetch* button will be replaced by a *push* button. Press it ASAP to upload your changes!
