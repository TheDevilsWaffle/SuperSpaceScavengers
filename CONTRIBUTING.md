<h1>Contributing to <em>Super Space Scavengers</em></h1>
<p>Looking to contribute something to <em>Super Space Scavengers</em>? <strong>Here&rsquo;s how you can help.</strong></p>

<h2>First and Foremost</h2>
<p>Are you brand new to Github? Have you used the command line before but forgot how? Are you new to version control in general? Fear not! There are tons of resources to learning how to use Github and also the command line (which is more reliable than the windows app, especially when merge conflicts arise).</p>
<p>If you are entirely brand new to Github, then may I recommend reading the <a href="https://help.github.com/articles/getting-started-with-github-for-windows" target="_blank">Getting Started</a> at Github&rsquo;s website or even <a href="http://arstechnica.com/information-technology/2012/05/hands-on-github-for-windows-takes-the-pain-out-of-using-git/" target="_blank">this nifty explantion</a>? These are both pretty good step-by-step instructions for how to use Github, but if that is not enough, let me introduce you to the command line.</p>
<p>Once you understand and use the command line in the windows app, you probably will not want to go back to the push buttons on the GUI. This is simply because the ease of using the command line and the reliability of it makes it much less of a hassle. You just have to believe me or suffer the wrath of the merge conflicts that you will have otherwise. Anyways, follow these simple steps:</p>
<ul>
    <li><code>git pull</code> &mdash; Used to pull any and all changes from github</li>
    <li><code>git add -A</code> &mdash; Used to add all untracked files to git<li>
    <li><code>git commit</code> &mdash; Used to create a local snapshot of current changes</li>
    <li><code>git pull</code> &mdash; Used as a double-check for all recent changes (someone could've pushed before you)</li>
    <li><code>git push</code> &mdash; Used to upload commits to github.</li>
</ul>
<p>What does this mean? This is a standard way of working with version control and it means that you should start working with a <strong>PULL</strong> from the latest version of files from the master repo to your local repo, <strong>WORK</strong> on your stuff in the local repo, and <strong>COMMIT</strong> your files to your local repo often (cannot stress this enough). When you think you are ready to add your newly created files to the master repo then you should <strong>PULL</strong> from it ONE MORE TIME to make sure you for sure have the LATEST VERSION of everything (remember, other people are working on these files and might have updated them, even 10 seconds after your last pull). After you <strong>PULL</strong>, then you make your <strong>PUSH</strong> to the master repo with your files.</p>


<h2>Using the Shell/Git</h2>
<img src="https://github.global.ssl.fastly.net/images/modules/logos_page/GitHub-Logo.png" alt="Github Logo" />
<p>As we all know, using version control can be quite confusing. This section is a work-in-progress explanation of how to do more advanced things in the git shell.</p>
<ul>
<li>
<h3>Branches</h3>
<p>Working with branches allows you to workin in an isolated environment, away from the master. This is a good practice because working straight off the master branch can cause issues for everyone if your work has errors. Remember to limit yourself on what you work on in any one particular branch. For example creating a branch called "level4Design" shoud only contain work relating to the name of the branch. If an enemy's AI needs to be completly changed or a physics script needs to be refactored, then a different branch should be used. This is important for isolating probelms with merging. Creating a branch is as easy as doing the following:</p>
<code>| git checkout -b myNewBranch</code>.
<p>What is this command doing? This command is doing two things at once: it is creating a new branch called "myNewBranch" and is switching over to working on it with the "checkout" command. From here on, you are working in your new branch.</p>
<p>To switch back and forth between branches, just use the <code>git checkout &lt;branch&gt;</code> command, example: <code>git checkout myNewBranch</code> - this switches to the myNewBranch.</p>
<ul>
<li>
<h4>Advanced&mdash;Tracking the Master</h4>
<p>When you pull from this branch you will not receive any updates from the master branch. If you want to have your new branch track changes made in the master branch, then you need to do the following command:</p>
<code>git branch -l -f myNewBranch -t master</code>
<p>This command will set the branch "myNewBranch" to track the master branch so that when the command <code>git pull</code> is performed, it will check with the master and pull changes made from there.</p>
</li>
</ul>
<li>
<h3>Merging</h3>
<p>Before solving a merge problem you want to run the following command in git to set up your merge tool. I recommend using "tortoisemerge":</p>
<code>git config merge.tool tortoisemerge</code>
<p>When it is time to add your branch to the master repo, a merge needs to be performed. To start the merge use the following commands:</p>
<code>git checkout master</code><br />
<code>git merge myNewBranch</code>
<p>These commands will switch you over to the master branch, and then merge "myNewBranch" to the master.</p>
<ul>
<li>
<h4>Advanced&mdash;Merge Conflicts</h4>
<p>Merge conflicts are going to happen. When they do happen, do not fear, because the power of version control is in how easily conflicts can be solved using the <code>git mergetool</code> command. To use it we have to do the following:</p>
<code>git mergetool --tool=&lt;name of tool&gt;</code>
<p>The <code>&lt;name of tool&gt;</code> part of this command can be specified by the user based on what tool they'd like to use. The computers at Digipen can use <code>tortoisemerge</code>, but <code>meld</code> should also work. These both perform the same function, but have different GUIs to help you solve merge conflicts. Basically, it will pull up a text editor program that will be divided into 3 sections: your code, the code at the master, and the new version of the code that you will decide to upload. By looking carefully between your code and the master code, update the code to upload carefully and save the changes. You may have to do this for several files. Once you have finished, you should be able to <code>git push</code> your code! Yay!</p> 
</li>
</ul>
<h3>Reverting</h3>

<p>Sometimes mistakes happen and the merge you just made does not work out well. Do not destroy your repo and lose all the work you have done. If you commit early and often you can pinpoint the exact things you want to get rid of by using the <code>git revert</code> command. For example, if you need to get rid of the last merge:</p>
<code>git revert -m 1 [SHA]</code>
<p>What is this code doing? the <code>git revert</code> part of the command tells us to use git&rsquo;s revert command, while the <code>-m 1</code> flag is used to declare that we&rsquo;ll revert back one parent-number, or SHA, of the merge. You can think of the SHA as a unique ID that is assigned to every commit and merge. a SHA is a 40-character checksum hash that looks a lot like this:</p>
<blockquote>0de4e535753f6c80ec5b1e2ccf426915a0036eaa</blockquote> <p>Thankfully you do not have to memorize this number and at the top of the github app there is an option to click to copy the SHA to the clipboard if you ever need it.</p>
</li>
</ul>

<h2>Reporting Issues</h2>

<p>We only accept issues that are bug reports or feature requests. Bugs must be isolated and reproducible problems. Please read the following guidelines before opening any issue:</p>

<ol>
<li>
<strong>Search for existing issues.</strong> Please avoid submitting a duplicate issue. Before submitting your issue, first check if someone else has reported the same issue. Moreover, the issue may have already been resolved.</li>
<li>
<strong>Create an isolated and reproducible test case.</strong> Be sure the problem exists in the code with a <a href="http://css-tricks.com/reduced-test-cases/">reduced test case</a> that should be included in each bug report.</li>
<li>
<strong>Share as much information as possible.</strong> Also include steps to reproduce the bug.</li>
</ol>

<h2>Pull Requests</h2>
<p>Pull requests should always be against the <code>master</code> branch.</p>

<h2>Coding Standards</h2>

<p>TBA&hellip;</p>

<h2>Further Information</h2>

<p>For further documentation please refer to <a href="http://git-scm.com/book/en/Getting-Started-Git-Basics">Git Basics</a>.</p>
