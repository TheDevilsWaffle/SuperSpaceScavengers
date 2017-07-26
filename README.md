<h1>SuperSpaceScavengers</h1>

<h2>Engine</h2>
<p>Unity3D</p>

<h2>Concept</h2>
<p><em>Super Space Scavengers</em> is a 2D, top-down, tactical action game where players fight to strip an abandoned spaceship of its most valuable loot/parts before the ship becomes completely unstable and explodes.</p>

<h2>Core Features</h2>
<p><em>Super Space Scavengers</em> will focus on the following core features:</p>
<ul>
 	<li>Mechanically simple with many mechanical interactions (Environment/equipment)</li>
 	<li>Fast and Frantic gameplay</li>
 	<li>Procedurally Generated Spaceships (Levels)</li>
 	<li>Progression, keeping players playing more round and on into more games</li>
 </ul> 

<h2>Theme</h2>
<p><em>Super Space Scavengers</em> takes place in a futuristic science-fiction setting. While space travel and furturistic technology exists, it is not always bright, clean, and new technology. The world is scrappy and more wild west-like, with space ships being held together with the equivilent of duct tape and a prayer. Good parts are hard to come by and maintaining what you got is the difference between death and survival. Right now there are 3 planned factions that will determine the type of loot and layout type of a ship:</p>
<ul>
    <li>
        <h3>Human/Terran</h3>
        <p>Very organized, geometric/mirrored layouts</p>
    </li>
    <li>
        <h3>Alien</h3>
        <p>Organic layouts, dangerous/unsafe chemicals</p>
    </li>
    <li>
        <h3>Robot</h3>
        <p>Untraditional layouts, massive, sprawling.</p>
    </li>
</ul>

<h2>Genre</h2>
<p><em>Super Space Scavengers</em> is a singleplayer/multiplayer 2D, top-down, tactical action game.</p>

<h2>Mechanics</h2>
<p><sub>Super Space Scavengers recommends using an Xbox controller, but also supports keyboard controls</sub></p>
<ul>
	<li>
		<h3>Player Mechanics</h3>
		<ul>
			<li>
				<strong>Movement</strong>
				<p>2D free-movement using the left analog stick</p>
			</li>
			<li>
				<strong>Aiming</strong>
				<p>8-directional (or free) aiming using the right analog stick (twin-stick shooter style)</p>
			</li>
			<li>
				<strong>Pickup/Drop Item</strong>
				<p>X button</p>
			</li>
            <li>
                <strong>Throw Carried Item</strong>
                <p>Right Bumper</p>
            </li>
			<li>
				<strong>Use Item</strong>
				<p>Right Trigger</p>
			</li>
			<li>
				<strong>Cycle/Switch Items</strong>
				<p>Y button</p>
			</li>
            <li>
                <strong>Contextual Interaction</strong>
                <p>A button</p>
            </li>
		</ul>
	</li>
	<li>
		<h3>Additional Mechanics</h3>
		<p>...</p>
	</li>
	<li>
		<h3>Additional Mechanics</h3>
		<p>...</p>
	</li>
</ul>

<h2>Environment</h2>
<ul>
    <li>
        <h3>Goals</h3>
        <p>The environment should make sense for areas that are clearly defined as passable hallways and rooms. The game manager needs to understand what is happening in rooms/hallways/space at all times. The environment needs to understand the current state of power in rooms/hallways in order for the right interactions to take place. Layout generation for ships can vary depending upon faction (organized for humans, organic for alien, etc.)</p>
    </li>
    <li>
        <h3>Layout Generation</h3>
        <p>Layout generation is the most complicated thing, and will need constant testing to find out what works and what doesn’t.</p>
    </li>
    <li>
        <h3>Power</h3>
        <p>Powerlines are not always exposed (items like an X-Ray machines can help you follow the trail). Power lines can be exposed naturally and even not at all. Powerlines, when powered, pulse every so often.</p>
    </li>
    <li>
        <h3>Room/Hallway Types</h3>
        <p>The rooms and hallways on the ship need to have a purpose and make sense for why they exist on the ship in order to create a believable space. The rooms/hallways will have functions/purposes such as:</p>
        <ul>
            <li>Bridge / Command Center / Piloting section</li>
            <li>Engine Room / Reactor (could be multiple)</li>
            <li>Weapons Bay</li>
            <li>Life Support Area (Oxygen)</li>
            <li>Doctor/Medical Area</li>
            <li>Science Labs</li>
            <li>Alien Egg Incubation Room</li>
            <li>Brig</li>
            <li>Cargo Bay(s)</li>
            <li>Docking Station</li>
            <li>Crew Quarters</li>
            <li>Elevators</li>
            <li>Traversal Tubes (gravity shift)</li>
            <li>Mess Hall</li>
            <li>Rec Center</li>
            <li>Janitor’s Closet</li>
            <li>Random Important Electrical Systems</li>
            <li>Bathrooms</li>
            <li>Garbage Compactor</li>
            <li>Hanger Bay</li>
            <li>Maintenance Tunnels</li>
        </ul>
    </li>
    <li>
        <h3>Loot Types</h3>
        <p>Players will want to board a ship and loot any and all valuable items aboard the ship. What is the most valuable type of loot will change from round to round. Nearly all loot types serve a purpose aboard the ship and can cause environmental interactions (positive/negative) if removed. The folling loot types are currently planned for: </p>
        <ul>
            <ul>
                <li>Power Sources (Batteries, Generators, Reactor Cores, Power Crystals, Fuel Canisters, etc.)</li>
                <li>Weapons (Personal Weapons, Weapon Systems, Torpedo Tubes, Laser Crystals, Nuclear Fuel, etc.)</li>
                <li>Computer Systems (Data Logs, Weapon Systems, Flight Controller, Main Computer, Communications Systems, Power Regulator, etc.)</li>
                <li>Currency (SpaceBucks, Gold, Rare Metals, Crystals, Gems, Bitcoins, etc.)</li>
                <li>Medical Supplies (Med Packs, Nano-Boosters, Pills, Medical Tools, Oxygen Canisters, etc.)</li>
                <li>Science Discoveries (Science Logs, Books, Baby Alien, Rare Minerals, etc.)</li>
                <li>Highly Radioactive/Explosive Materials (be careful, they can go boom if dropped)</li>
                <li>Cargo Crates/Barrels hide what is currently in them (can have an item or nothing at all)</li>
            </ul>
        </ul>
    </li>
    <li>
        <h3>Environmental Interactions</h3>
        <p>A lot of the excitement in <em>Super Space Scavengers</em> comes from just how quickly things can go wrong when systems start to fail on a damaged spaceship. The following list are planned events/interactions that can occur:</p>
        <ul>
            <li>Gravity On/Off</li>
            <li>Fire (can spread)</li>
            <li>Power Loss/Power Overload</li>
            <li>Lights Deactivated/Activated</li>
            <li>Self-Destruct Sequence</li>
            <li>Prison Ship Riot (release of enemies)</li>
            <li>Distress Beacon (enemies will beam aboard or attack the ship)</li>
            <li>Irradiation (AOE of ticking damage)</li>
            <li>Cryochambers Deactivated (crew/enemies will emerge)</li>
            <li>Alien (yes, that one from that movie)</li>
            <li>Lifesupport Failure (Oxygen loss)</li>
            <li>Emergency Decompression (Getting sucked into space)</li>
            <li>more to come…</li>
        </ul>
    </li>
</ul>