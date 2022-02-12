using System.Collections.Generic;

namespace Dolo.PlanetAI;

public sealed class StatusEmote
{
	public static readonly Dictionary<StatusEmote, string> Emotes = new Dictionary<StatusEmote, string>
	{
		{ msp_heart, msp_heart.Emote },
		{ msp_famestar, msp_famestar.Emote },
		{ msp_starcoin, msp_starcoin.Emote },
		{ msp_diamond, msp_diamond.Emote },
		{ msp_present, msp_present.Emote },
		{ msp_loveit, msp_loveit.Emote },
		{ msp_autograph, msp_autograph.Emote },
		{ msp_movie, msp_movie.Emote },
		{ msp_artbook, msp_artbook.Emote },
		{ msp_logo, msp_logo.Emote },
		{ msp_popcorn, msp_popcorn.Emote },
		{ blockstar_new, blockstar_new.Emote },
		{ blockstar_cool, blockstar_cool.Emote },
		{ bp_loveit, bp_loveit.Emote },
		{ bp_yay, bp_yay.Emote },
		{ pixi_smile, pixi_smile.Emote },
		{ pixi_omg, pixi_omg.Emote },
		{ pixi_unamused, pixi_unamused.Emote },
		{ pixi_yay, pixi_yay.Emote },
		{ pixi_wink, pixi_wink.Emote },
		{ zac_smile, zac_smile.Emote },
		{ zac_omg, zac_omg.Emote },
		{ zac_unamused, zac_unamused.Emote },
		{ zac_yay, zac_yay.Emote },
		{ zac_wink, zac_wink.Emote },
		{ boonie_smile, boonie_smile.Emote },
		{ boonie_inlove, boonie_inlove.Emote },
		{ boonie_happy, boonie_happy.Emote },
		{ boonie_cry, boonie_cry.Emote },
		{ boonie_angry, boonie_angry.Emote },
		{ boonie_lol, boonie_lol.Emote },
		{ boonie_love, boonie_love.Emote },
		{ boonie_meh, boonie_meh.Emote },
		{ boonie_devil, boonie_devil.Emote },
		{ boonie_wink, boonie_wink.Emote },
		{ smile, smile.Emote },
		{ smiley, smiley.Emote },
		{ sad, sad.Emote },
		{ grin, grin.Emote },
		{ angel, angel.Emote },
		{ devil, devil.Emote },
		{ knockedout, knockedout.Emote },
		{ cool, cool.Emote },
		{ teeth, teeth.Emote },
		{ happy, happy.Emote },
		{ nervous, nervous.Emote },
		{ verysad, verysad.Emote },
		{ wasntme, wasntme.Emote },
		{ crying, crying.Emote },
		{ push, push.Emote },
		{ winktounge, winktounge.Emote },
		{ laughing, laughing.Emote },
		{ worried, worried.Emote },
		{ surprised, surprised.Emote },
		{ love, love.Emote },
		{ sweat, sweat.Emote },
		{ wink, wink.Emote },
		{ whistle, whistle.Emote },
		{ silent, silent.Emote },
		{ question, question.Emote },
		{ howling, howling.Emote },
		{ comicbook_smile, comicbook_smile.Emote },
		{ comicbook_bigsmile, comicbook_bigsmile.Emote },
		{ comicbook_wink, comicbook_wink.Emote },
		{ comicbook_superhappy, comicbook_superhappy.Emote },
		{ comicbook_ew, comicbook_ew.Emote },
		{ comicbook_crying, comicbook_crying.Emote },
		{ comicbook_surprised, comicbook_surprised.Emote },
		{ comicbook_what, comicbook_what.Emote },
		{ comicbook_scars, comicbook_scars.Emote },
		{ comicbook_eyepatch, comicbook_eyepatch.Emote },
		{ comicbook_moustache, comicbook_moustache.Emote },
		{ comicbook_blind, comicbook_blind.Emote },
		{ comicbook_oh, comicbook_oh.Emote },
		{ comicbook_angry, comicbook_angry.Emote },
		{ comicbook_crazy, comicbook_crazy.Emote },
		{ comicbook_omg, comicbook_omg.Emote },
		{ comicbook_sick, comicbook_sick.Emote },
		{ comicbook_tongue, comicbook_tongue.Emote },
		{ comicbook_tired, comicbook_tired.Emote },
		{ comicbook_devil, comicbook_devil.Emote },
		{ comicbook_angel, comicbook_angel.Emote },
		{ comicbook_love, comicbook_love.Emote },
		{ comicbook_summer, comicbook_summer.Emote },
		{ comicbook_huh, comicbook_huh.Emote },
		{ pirate, pirate.Emote },
		{ inlove, inlove.Emote },
		{ coolgrin, coolgrin.Emote },
		{ wondering, wondering.Emote },
		{ cheeky, cheeky.Emote },
		{ yell, cheeky.Emote },
		{ music, cheeky.Emote },
		{ angry, cheeky.Emote },
		{ guitar, cheeky.Emote },
		{ sunshine, cheeky.Emote },
		{ sleepy, cheeky.Emote },
		{ lick, cheeky.Emote },
		{ kiss, cheeky.Emote },
		{ sick, cheeky.Emote },
		{ yes, cheeky.Emote },
		{ idea, idea.Emote },
		{ no, no.Emote },
		{ moustache, moustache.Emote },
		{ surprise, surprise.Emote },
		{ sleeping, sleeping.Emote },
		{ painting, painting.Emote },
		{ student, student.Emote },
		{ greedy, greedy.Emote },
		{ beaten, beaten.Emote },
		{ phone, phone.Emote },
		{ comicbook_annoyed, comicbook_annoyed.Emote },
		{ comicbook_argh, comicbook_argh.Emote },
		{ comicbook_yawn, comicbook_yawn.Emote },
		{ comicbook_grin, comicbook_grin.Emote },
		{ comicbook_boo, comicbook_boo.Emote },
		{ comicbook_cool, comicbook_cool.Emote },
		{ comicbook_hero, comicbook_hero.Emote },
		{ comicbook_cheeks, comicbook_cheeks.Emote },
		{ comicbook_hmm, comicbook_hmm.Emote },
		{ comicbook_vomit, comicbook_vomit.Emote },
		{ comicbook_well, comicbook_well.Emote },
		{ comicbook_weird, comicbook_weird.Emote },
		{ comicbook_happy, comicbook_happy.Emote },
		{ comicbook_sad, comicbook_sad.Emote },
		{ comicbook_yeah, comicbook_yeah.Emote },
		{ comicbook_silent, comicbook_silent.Emote },
		{ comicbook_wow, comicbook_wow.Emote },
		{ comicbook_yeahright, comicbook_yeahright.Emote },
		{ comicbook_worried, comicbook_worried.Emote },
		{ comicbook_ohyeah, comicbook_ohyeah.Emote },
		{ comicbook_lady, comicbook_lady.Emote },
		{ comicbook_ladysmile, comicbook_ladysmile.Emote },
		{ comicbook_ladybigsmile, comicbook_ladybigsmile.Emote },
		{ hearts_smile, hearts_smile.Emote },
		{ hearts_bigsmile, hearts_bigsmile.Emote },
		{ hearts_wink, hearts_wink.Emote },
		{ hearts_superhappy, hearts_superhappy.Emote },
		{ hearts_ew, hearts_ew.Emote },
		{ hearts_crying, hearts_crying.Emote },
		{ hearts_surprised, hearts_surprised.Emote },
		{ hearts_what, hearts_what.Emote },
		{ hearts_scars, hearts_scars.Emote },
		{ hearts_eyepatch, hearts_eyepatch.Emote },
		{ hearts_moustache, hearts_moustache.Emote },
		{ hearts_blind, hearts_blind.Emote },
		{ hearts_oh, hearts_oh.Emote },
		{ hearts_angry, hearts_angry.Emote },
		{ hearts_crazy, hearts_crazy.Emote },
		{ hearts_omg, hearts_omg.Emote },
		{ hearts_sick, hearts_sick.Emote },
		{ hearts_tongue, hearts_tongue.Emote },
		{ hearts_tired, hearts_tired.Emote },
		{ hearts_devil, hearts_devil.Emote },
		{ hearts_angel, hearts_angel.Emote },
		{ hearts_love, hearts_love.Emote },
		{ hearts_summer, hearts_summer.Emote },
		{ hearts_huh, hearts_huh.Emote },
		{ hearts_annoyed, hearts_annoyed.Emote },
		{ hearts_argh, hearts_argh.Emote },
		{ hearts_yawn, hearts_yawn.Emote },
		{ hearts_grin, hearts_grin.Emote },
		{ hearts_boo, hearts_boo.Emote },
		{ hearts_cool, hearts_cool.Emote },
		{ hearts_hero, hearts_hero.Emote },
		{ hearts_cheeks, hearts_cheeks.Emote },
		{ hearts_hmm, hearts_hmm.Emote },
		{ hearts_vomit, hearts_vomit.Emote },
		{ hearts_well, hearts_well.Emote },
		{ hearts_weird, hearts_weird.Emote },
		{ hearts_happy, hearts_happy.Emote },
		{ hearts_sad, hearts_sad.Emote },
		{ hearts_yeah, hearts_yeah.Emote },
		{ hearts_silent, hearts_silent.Emote },
		{ hearts_wow, hearts_wow.Emote },
		{ hearts_yeahright, hearts_yeahright.Emote },
		{ hearts_worried, hearts_worried.Emote },
		{ hearts_ohyeah, hearts_ohyeah.Emote },
		{ hearts_lady, hearts_lady.Emote },
		{ hearts_ladysmile, hearts_ladysmile.Emote },
		{ hearts_ladybigsmile, hearts_ladybigsmile.Emote },
		{ dj_cool, dj_cool.Emote },
		{ dj_oops, dj_oops.Emote },
		{ dj_happy, dj_happy.Emote },
		{ dj_wow, dj_wow.Emote },
		{ dj_scratch, dj_scratch.Emote },
		{ dj_girl, dj_girl.Emote },
		{ dj_mic, dj_mic.Emote },
		{ dj_yeah, dj_yeah.Emote },
		{ dj_ssh, dj_ssh.Emote },
		{ octo_umbrella, octo_umbrella.Emote },
		{ octo_fun, octo_fun.Emote },
		{ octo_sad, octo_sad.Emote },
		{ octo_oh, octo_oh.Emote },
		{ octo_what, octo_what.Emote },
		{ octo_argh, octo_argh.Emote },
		{ octo_angry, octo_angry.Emote },
		{ octo_yummy, octo_yummy.Emote },
		{ octo_hi, octo_hi.Emote },
		{ octo_bye, octo_bye.Emote },
		{ octo_hmf, octo_hmf.Emote },
		{ octo_zzz, octo_zzz.Emote },
		{ santa_smile, santa_smile.Emote },
		{ santa_bigsmile, santa_bigsmile.Emote },
		{ santa_wink, santa_wink.Emote },
		{ santa_superhappy, santa_superhappy.Emote },
		{ santa_ew, santa_ew.Emote },
		{ santa_crying, santa_crying.Emote },
		{ santa_surprised, santa_surprised.Emote },
		{ santa_scars, santa_scars.Emote },
		{ santa_patch, santa_patch.Emote },
		{ santa_moustache, santa_moustache.Emote },
		{ santa_blind, santa_blind.Emote },
		{ santa_oh, santa_oh.Emote },
		{ santa_angry, santa_angry.Emote },
		{ santa_crazy, santa_crazy.Emote },
		{ santa_sick, santa_sick.Emote },
		{ santa_tongue, santa_tongue.Emote },
		{ santa_tired, santa_tired.Emote },
		{ santa_devil, santa_devil.Emote },
		{ santa_angel, santa_angel.Emote },
		{ santa_love, santa_love.Emote },
		{ santa_summer, santa_summer.Emote },
		{ santa_annoyed, santa_annoyed.Emote },
		{ santa_argh, santa_argh.Emote },
		{ santa_yawn, santa_yawn.Emote },
		{ santa_grin, santa_grin.Emote },
		{ santa_boo, santa_boo.Emote },
		{ santa_yeah, santa_yeah.Emote },
		{ santa_worried, santa_worried.Emote },
		{ santa_vomit, santa_vomit.Emote },
		{ santa_well, santa_well.Emote },
		{ santa_weird, santa_weird.Emote },
		{ santa_happy, santa_happy.Emote },
		{ santa_sad, santa_sad.Emote },
		{ santa_wow, santa_wow.Emote },
		{ santa_omg, santa_omg.Emote },
		{ santa_yeahright, santa_yeahright.Emote },
		{ santa_hmm, santa_hmm.Emote },
		{ santa_cheeks, santa_cheeks.Emote },
		{ santa_silent, santa_silent.Emote },
		{ santa_hero, santa_hero.Emote },
		{ santa_ohyeah, santa_ohyeah.Emote },
		{ santa_huh, santa_huh.Emote },
		{ santa_lady, santa_lady.Emote },
		{ santa_ladysmile, santa_ladysmile.Emote },
		{ santa_ladybigsmile, santa_ladybigsmile.Emote },
		{ square_clown, square_clown.Emote },
		{ square_glad, square_glad.Emote },
		{ square_bigsmile, square_bigsmile.Emote },
		{ square_sick, square_sick.Emote },
		{ square_angry, square_angry.Emote },
		{ square_devil, square_devil.Emote },
		{ square_baby, square_baby.Emote },
		{ square_phone, square_phone.Emote },
		{ square_vomit, square_vomit.Emote },
		{ square_argh, square_argh.Emote },
		{ square_point, square_point.Emote },
		{ square_nice, square_nice.Emote },
		{ square_tongue, square_tongue.Emote },
		{ square_hug, square_hug.Emote },
		{ square_sad, square_sad.Emote },
		{ square_hmm, square_hmm.Emote },
		{ square_crying, square_crying.Emote },
		{ square_love, square_love.Emote },
		{ square_wink, square_wink.Emote },
		{ square_birthday, square_birthday.Emote },
		{ square_silent, square_silent.Emote },
		{ square_drool, square_drool.Emote },
		{ square_sadbaby, square_sadbaby.Emote },
		{ square_cool, square_cool.Emote },
		{ square_kiss, square_kiss.Emote },
		{ square_happy, square_happy.Emote },
		{ square_surprised, square_surprised.Emote },
		{ square_tired, square_tired.Emote },
		{ square_yawn, square_yawn.Emote },
		{ square_hand, square_hand.Emote },
		{ square_angel, square_angel.Emote },
		{ square_zzz, square_zzz.Emote }
	};

	public readonly string Emote;

	public static StatusEmote msp_heart => new StatusEmote("(msp_heart)");

	public static StatusEmote msp_famestar => new StatusEmote("(msp_famestar)");

	public static StatusEmote msp_starcoin => new StatusEmote("(msp_starcoin)");

	public static StatusEmote msp_diamond => new StatusEmote("(msp_diamond)");

	public static StatusEmote msp_present => new StatusEmote("(msp_present)");

	public static StatusEmote msp_loveit => new StatusEmote("(msp_loveit)");

	public static StatusEmote msp_autograph => new StatusEmote("(msp_autograph)");

	public static StatusEmote msp_movie => new StatusEmote("(msp_movie)");

	public static StatusEmote msp_artbook => new StatusEmote("(msp_artbook)");

	public static StatusEmote msp_logo => new StatusEmote("(msp_logo)");

	public static StatusEmote msp_popcorn => new StatusEmote("(msp_popcorn)");

	public static StatusEmote blockstar_new => new StatusEmote("(blockstar_new)");

	public static StatusEmote blockstar_cool => new StatusEmote("(blockstar_cool)");

	public static StatusEmote bp_loveit => new StatusEmote("(bp_loveit)");

	public static StatusEmote bp_yay => new StatusEmote("(bp_yay)");

	public static StatusEmote pixi_smile => new StatusEmote("(pixi_smile)");

	public static StatusEmote pixi_omg => new StatusEmote("(pixi_omg)");

	public static StatusEmote pixi_unamused => new StatusEmote("(pixi_unamused)");

	public static StatusEmote pixi_yay => new StatusEmote("(pixi_yay)");

	public static StatusEmote pixi_wink => new StatusEmote("(pixi_wink)");

	public static StatusEmote zac_smile => new StatusEmote("(zac_smile)");

	public static StatusEmote zac_omg => new StatusEmote("(zac_omg)");

	public static StatusEmote zac_unamused => new StatusEmote("(zac_unamused)");

	public static StatusEmote zac_yay => new StatusEmote("(zac_yay)");

	public static StatusEmote zac_wink => new StatusEmote("(zac_wink)");

	public static StatusEmote boonie_smile => new StatusEmote("(boonie_smile)");

	public static StatusEmote boonie_inlove => new StatusEmote("(boonie_inlove)");

	public static StatusEmote boonie_happy => new StatusEmote("(boonie_happy)");

	public static StatusEmote boonie_cry => new StatusEmote("(boonie_cry)");

	public static StatusEmote boonie_angry => new StatusEmote("(boonie_angry)");

	public static StatusEmote boonie_lol => new StatusEmote("(boonie_lol)");

	public static StatusEmote boonie_love => new StatusEmote("(boonie_love)");

	public static StatusEmote boonie_meh => new StatusEmote("(boonie_meh)");

	public static StatusEmote boonie_devil => new StatusEmote("(boonie_devil)");

	public static StatusEmote boonie_wink => new StatusEmote("(boonie_wink)");

	public static StatusEmote smile => new StatusEmote(":]");

	public static StatusEmote smiley => new StatusEmote(":)");

	public static StatusEmote sad => new StatusEmote(":(");

	public static StatusEmote grin => new StatusEmote("|B");

	public static StatusEmote angel => new StatusEmote("(a)");

	public static StatusEmote devil => new StatusEmote("(d)");

	public static StatusEmote knockedout => new StatusEmote("(s)");

	public static StatusEmote cool => new StatusEmote("B)");

	public static StatusEmote teeth => new StatusEmote(":B");

	public static StatusEmote happy => new StatusEmote(":))");

	public static StatusEmote nervous => new StatusEmote(":E(");

	public static StatusEmote verysad => new StatusEmote(":((");

	public static StatusEmote wasntme => new StatusEmote("(i)");

	public static StatusEmote crying => new StatusEmote(";(");

	public static StatusEmote push => new StatusEmote("X(");

	public static StatusEmote winktounge => new StatusEmote(";p");

	public static StatusEmote laughing => new StatusEmote(":D");

	public static StatusEmote worried => new StatusEmote(":s");

	public static StatusEmote surprised => new StatusEmote(":o");

	public static StatusEmote love => new StatusEmote("(l)");

	public static StatusEmote sweat => new StatusEmote("'':s");

	public static StatusEmote wink => new StatusEmote(";)");

	public static StatusEmote whistle => new StatusEmote("-B");

	public static StatusEmote silent => new StatusEmote(":x");

	public static StatusEmote question => new StatusEmote(":?s");

	public static StatusEmote howling => new StatusEmote(":'(");

	public static StatusEmote comicbook_smile => new StatusEmote("(comic_smile)");

	public static StatusEmote comicbook_bigsmile => new StatusEmote("(comic_bigsmile)");

	public static StatusEmote comicbook_wink => new StatusEmote("(comic_wink)");

	public static StatusEmote comicbook_superhappy => new StatusEmote("(comic_superhappy)");

	public static StatusEmote comicbook_ew => new StatusEmote("(comic_ew)");

	public static StatusEmote comicbook_crying => new StatusEmote("(comic_crying)");

	public static StatusEmote comicbook_surprised => new StatusEmote("(comic_surprised)");

	public static StatusEmote comicbook_what => new StatusEmote("(comic_what)");

	public static StatusEmote comicbook_scars => new StatusEmote("(comic_scars)");

	public static StatusEmote comicbook_eyepatch => new StatusEmote("(comic_eyepatch)");

	public static StatusEmote comicbook_moustache => new StatusEmote("(comic_moustache)");

	public static StatusEmote comicbook_blind => new StatusEmote("(comic_blind)");

	public static StatusEmote comicbook_oh => new StatusEmote("(comic_oh)");

	public static StatusEmote comicbook_angry => new StatusEmote("(comic_angry)");

	public static StatusEmote comicbook_crazy => new StatusEmote("(comic_crazy)");

	public static StatusEmote comicbook_omg => new StatusEmote("(comic_omg)");

	public static StatusEmote comicbook_sick => new StatusEmote("(comic_sick)");

	public static StatusEmote comicbook_tongue => new StatusEmote("(comic_tongue)");

	public static StatusEmote comicbook_tired => new StatusEmote("(comic_tired)");

	public static StatusEmote comicbook_devil => new StatusEmote("(comic_devil)");

	public static StatusEmote comicbook_angel => new StatusEmote("(comic_angel)");

	public static StatusEmote comicbook_love => new StatusEmote("(comic_love)");

	public static StatusEmote comicbook_summer => new StatusEmote("(comic_summer)");

	public static StatusEmote comicbook_huh => new StatusEmote("(comic_huh)");

	public static StatusEmote pirate => new StatusEmote("(p)");

	public static StatusEmote inlove => new StatusEmote("EE&gt;");

	public static StatusEmote coolgrin => new StatusEmote("B))");

	public static StatusEmote wondering => new StatusEmote(":^)");

	public static StatusEmote cheeky => new StatusEmote(":p");

	public static StatusEmote yell => new StatusEmote("x-o");

	public static StatusEmote music => new StatusEmote("C)");

	public static StatusEmote angry => new StatusEmote(":@");

	public static StatusEmote guitar => new StatusEmote("(gt)");

	public static StatusEmote sunshine => new StatusEmote("(#)");

	public static StatusEmote sleepy => new StatusEmote("|O");

	public static StatusEmote lick => new StatusEmote(";b");

	public static StatusEmote kiss => new StatusEmote(":*");

	public static StatusEmote sick => new StatusEmote(":F");

	public static StatusEmote yes => new StatusEmote("(y)");

	public static StatusEmote idea => new StatusEmote("(!)");

	public static StatusEmote no => new StatusEmote("(n)");

	public static StatusEmote moustache => new StatusEmote("(m)");

	public static StatusEmote surprise => new StatusEmote("(bo)");

	public static StatusEmote sleeping => new StatusEmote("|)");

	public static StatusEmote painting => new StatusEmote("(pn)");

	public static StatusEmote student => new StatusEmote("&lt;:)");

	public static StatusEmote greedy => new StatusEmote("$)");

	public static StatusEmote beaten => new StatusEmote("(b)");

	public static StatusEmote phone => new StatusEmote("(t)");

	public static StatusEmote comicbook_annoyed => new StatusEmote("(comic_annoyed)");

	public static StatusEmote comicbook_argh => new StatusEmote("(comic_argh)");

	public static StatusEmote comicbook_yawn => new StatusEmote("(comic_yawn)");

	public static StatusEmote comicbook_grin => new StatusEmote("(comic_grin)");

	public static StatusEmote comicbook_boo => new StatusEmote("(comic_boo)");

	public static StatusEmote comicbook_cool => new StatusEmote("(comic_cool)");

	public static StatusEmote comicbook_hero => new StatusEmote("(comic_hero)");

	public static StatusEmote comicbook_cheeks => new StatusEmote("(comic_cheeks)");

	public static StatusEmote comicbook_hmm => new StatusEmote("(comic_hmm)");

	public static StatusEmote comicbook_vomit => new StatusEmote("(comic_vomit)");

	public static StatusEmote comicbook_well => new StatusEmote("(comic_well)");

	public static StatusEmote comicbook_weird => new StatusEmote("(comic_weird)");

	public static StatusEmote comicbook_happy => new StatusEmote("(comic_happy)");

	public static StatusEmote comicbook_sad => new StatusEmote("(comic_sad)");

	public static StatusEmote comicbook_yeah => new StatusEmote("(comic_yeah)");

	public static StatusEmote comicbook_silent => new StatusEmote("(comic_silent)");

	public static StatusEmote comicbook_wow => new StatusEmote("(comic_wow)");

	public static StatusEmote comicbook_yeahright => new StatusEmote("(comic_yeahright)");

	public static StatusEmote comicbook_worried => new StatusEmote("(comic_worried)");

	public static StatusEmote comicbook_ohyeah => new StatusEmote("(comic_ohyeah)");

	public static StatusEmote comicbook_lady => new StatusEmote("(comic_lady)");

	public static StatusEmote comicbook_ladysmile => new StatusEmote("(comic_ladysmile)");

	public static StatusEmote comicbook_ladybigsmile => new StatusEmote("(comic_ladybigsmile)");

	public static StatusEmote hearts_smile => new StatusEmote("(hearts_smile)");

	public static StatusEmote hearts_bigsmile => new StatusEmote("(hearts_bigsmile)");

	public static StatusEmote hearts_wink => new StatusEmote("(hearts_wink)");

	public static StatusEmote hearts_superhappy => new StatusEmote("(hearts_superhappy)");

	public static StatusEmote hearts_ew => new StatusEmote("(hearts_ew)");

	public static StatusEmote hearts_crying => new StatusEmote("(hearts_crying)");

	public static StatusEmote hearts_surprised => new StatusEmote("(hearts_surprised)");

	public static StatusEmote hearts_what => new StatusEmote("(hearts_what)");

	public static StatusEmote hearts_scars => new StatusEmote("(hearts_scars)");

	public static StatusEmote hearts_eyepatch => new StatusEmote("(hearts_eyepatch)");

	public static StatusEmote hearts_moustache => new StatusEmote("(hearts_moustache)");

	public static StatusEmote hearts_blind => new StatusEmote("(hearts_blind)");

	public static StatusEmote hearts_oh => new StatusEmote("(hearts_oh)");

	public static StatusEmote hearts_angry => new StatusEmote("(hearts_angry)");

	public static StatusEmote hearts_crazy => new StatusEmote("(hearts_crazy)");

	public static StatusEmote hearts_omg => new StatusEmote("(hearts_omg)");

	public static StatusEmote hearts_sick => new StatusEmote("(hearts_sick)");

	public static StatusEmote hearts_tongue => new StatusEmote("(hearts_tongue)");

	public static StatusEmote hearts_tired => new StatusEmote("(hearts_tired)");

	public static StatusEmote hearts_devil => new StatusEmote("(hearts_devil)");

	public static StatusEmote hearts_angel => new StatusEmote("(hearts_angel)");

	public static StatusEmote hearts_love => new StatusEmote("(hearts_love)");

	public static StatusEmote hearts_summer => new StatusEmote("(hearts_summer)");

	public static StatusEmote hearts_huh => new StatusEmote("(hearts_huh)");

	public static StatusEmote hearts_annoyed => new StatusEmote("(hearts_annoyed)");

	public static StatusEmote hearts_argh => new StatusEmote("(hearts_argh)");

	public static StatusEmote hearts_yawn => new StatusEmote("(hearts_yawn)");

	public static StatusEmote hearts_grin => new StatusEmote("(hearts_grin)");

	public static StatusEmote hearts_boo => new StatusEmote("(hearts_boo)");

	public static StatusEmote hearts_cool => new StatusEmote("(hearts_cool)");

	public static StatusEmote hearts_hero => new StatusEmote("(hearts_hero)");

	public static StatusEmote hearts_cheeks => new StatusEmote("(hearts_cheeks)");

	public static StatusEmote hearts_hmm => new StatusEmote("(hearts_hmm)");

	public static StatusEmote hearts_vomit => new StatusEmote("(hearts_vomit)");

	public static StatusEmote hearts_well => new StatusEmote("(hearts_well)");

	public static StatusEmote hearts_weird => new StatusEmote("(hearts_weird)");

	public static StatusEmote hearts_happy => new StatusEmote("(hearts_happy)");

	public static StatusEmote hearts_sad => new StatusEmote("(hearts_sad)");

	public static StatusEmote hearts_yeah => new StatusEmote("(hearts_yeah)");

	public static StatusEmote hearts_silent => new StatusEmote("(hearts_silent)");

	public static StatusEmote hearts_wow => new StatusEmote("(hearts_wow)");

	public static StatusEmote hearts_yeahright => new StatusEmote("(hearts_yeahright)");

	public static StatusEmote hearts_worried => new StatusEmote("(hearts_worried)");

	public static StatusEmote hearts_ohyeah => new StatusEmote("(hearts_ohyeah)");

	public static StatusEmote hearts_lady => new StatusEmote("(hearts_lady)");

	public static StatusEmote hearts_ladysmile => new StatusEmote("(hearts_ladysmile)");

	public static StatusEmote hearts_ladybigsmile => new StatusEmote("(hearts_ladybigsmile)");

	public static StatusEmote dj_cool => new StatusEmote("(dj_cool)");

	public static StatusEmote dj_oops => new StatusEmote("(dj_oops)");

	public static StatusEmote dj_happy => new StatusEmote("(dj_happy)");

	public static StatusEmote dj_wow => new StatusEmote("(dj_wow)");

	public static StatusEmote dj_scratch => new StatusEmote("(dj_scratch)");

	public static StatusEmote dj_girl => new StatusEmote("(dj_girl)");

	public static StatusEmote dj_mic => new StatusEmote("(dj_mic)");

	public static StatusEmote dj_yeah => new StatusEmote("(dj_yeah)");

	public static StatusEmote dj_ssh => new StatusEmote("(dj_ssh)");

	public static StatusEmote octo_umbrella => new StatusEmote("(octo_umbrella)");

	public static StatusEmote octo_fun => new StatusEmote("(octo_fun)");

	public static StatusEmote octo_sad => new StatusEmote("(octo_sad)");

	public static StatusEmote octo_oh => new StatusEmote("(octo_oh)");

	public static StatusEmote octo_what => new StatusEmote("(octo_what)");

	public static StatusEmote octo_argh => new StatusEmote("(octo_argh)");

	public static StatusEmote octo_angry => new StatusEmote("(octo_angry)");

	public static StatusEmote octo_yummy => new StatusEmote("(octo_yummy)");

	public static StatusEmote octo_hi => new StatusEmote("(octo_hi)");

	public static StatusEmote octo_bye => new StatusEmote("(octo_bye)");

	public static StatusEmote octo_hmf => new StatusEmote("(octo_hmf)");

	public static StatusEmote octo_zzz => new StatusEmote("(octo_zzz)");

	public static StatusEmote santa_smile => new StatusEmote("(santa_smile)");

	public static StatusEmote santa_bigsmile => new StatusEmote("(santa_bigsmile)");

	public static StatusEmote santa_wink => new StatusEmote("(santa_wink)");

	public static StatusEmote santa_superhappy => new StatusEmote("(santa_superhappy)");

	public static StatusEmote santa_ew => new StatusEmote("(santa_ew)");

	public static StatusEmote santa_crying => new StatusEmote("(santa_crying)");

	public static StatusEmote santa_surprised => new StatusEmote("(santa_surprised)");

	public static StatusEmote santa_scars => new StatusEmote("(santa_scars)");

	public static StatusEmote santa_patch => new StatusEmote("(santa_patch)");

	public static StatusEmote santa_moustache => new StatusEmote("(santa_moustache)");

	public static StatusEmote santa_blind => new StatusEmote("(santa_blind)");

	public static StatusEmote santa_oh => new StatusEmote("(santa_oh)");

	public static StatusEmote santa_angry => new StatusEmote("(santa_angry)");

	public static StatusEmote santa_crazy => new StatusEmote("(santa_crazy)");

	public static StatusEmote santa_sick => new StatusEmote("(santa_sick)");

	public static StatusEmote santa_tongue => new StatusEmote("(santa_tongue)");

	public static StatusEmote santa_tired => new StatusEmote("(santa_tired)");

	public static StatusEmote santa_devil => new StatusEmote("(santa_devil)");

	public static StatusEmote santa_angel => new StatusEmote("(santa_angel)");

	public static StatusEmote santa_love => new StatusEmote("(santa_love)");

	public static StatusEmote santa_summer => new StatusEmote("(santa_summer)");

	public static StatusEmote santa_annoyed => new StatusEmote("(santa_annoyed)");

	public static StatusEmote santa_argh => new StatusEmote("(santa_argh)");

	public static StatusEmote santa_yawn => new StatusEmote("(santa_yawn)");

	public static StatusEmote santa_grin => new StatusEmote("(santa_grin)");

	public static StatusEmote santa_boo => new StatusEmote("(santa_boo)");

	public static StatusEmote santa_yeah => new StatusEmote("(santa_yeah)");

	public static StatusEmote santa_worried => new StatusEmote("(santa_worried)");

	public static StatusEmote santa_vomit => new StatusEmote("(santa_vomit)");

	public static StatusEmote santa_well => new StatusEmote("(santa_well)");

	public static StatusEmote santa_weird => new StatusEmote("(santa_weird)");

	public static StatusEmote santa_happy => new StatusEmote("(santa_happy)");

	public static StatusEmote santa_sad => new StatusEmote("(santa_sad)");

	public static StatusEmote santa_wow => new StatusEmote("(santa_wow)");

	public static StatusEmote santa_omg => new StatusEmote("(santa_omg)");

	public static StatusEmote santa_yeahright => new StatusEmote("(santa_yeahright)");

	public static StatusEmote santa_hmm => new StatusEmote("(santa_hmm)");

	public static StatusEmote santa_cheeks => new StatusEmote("(santa_cheeks)");

	public static StatusEmote santa_silent => new StatusEmote("(santa_silent)");

	public static StatusEmote santa_hero => new StatusEmote("(santa_hero)");

	public static StatusEmote santa_ohyeah => new StatusEmote("(santa_ohyeah)");

	public static StatusEmote santa_huh => new StatusEmote("(santa_huh)");

	public static StatusEmote santa_lady => new StatusEmote("(santa_lady)");

	public static StatusEmote santa_ladysmile => new StatusEmote("(santa_ladysmile)");

	public static StatusEmote santa_ladybigsmile => new StatusEmote("(santa_ladybigsmile)");

	public static StatusEmote square_clown => new StatusEmote("(square_clown)");

	public static StatusEmote square_glad => new StatusEmote("(square_glad)");

	public static StatusEmote square_bigsmile => new StatusEmote("(square_bigsmile)");

	public static StatusEmote square_sick => new StatusEmote("(square_sick)");

	public static StatusEmote square_angry => new StatusEmote("(square_angry)");

	public static StatusEmote square_devil => new StatusEmote("(square_devil)");

	public static StatusEmote square_baby => new StatusEmote("(square_baby)");

	public static StatusEmote square_phone => new StatusEmote("(square_phone)");

	public static StatusEmote square_vomit => new StatusEmote("(square_vomit)");

	public static StatusEmote square_argh => new StatusEmote("(square_argh)");

	public static StatusEmote square_point => new StatusEmote("(square_point)");

	public static StatusEmote square_nice => new StatusEmote("(square_nice)");

	public static StatusEmote square_tongue => new StatusEmote("(square_tongue)");

	public static StatusEmote square_hug => new StatusEmote("(square_hug)");

	public static StatusEmote square_sad => new StatusEmote("(square_sad)");

	public static StatusEmote square_hmm => new StatusEmote("(square_hmm)");

	public static StatusEmote square_crying => new StatusEmote("(square_crying)");

	public static StatusEmote square_love => new StatusEmote("(square_love)");

	public static StatusEmote square_wink => new StatusEmote("(square_wink)");

	public static StatusEmote square_birthday => new StatusEmote("(square_birthday)");

	public static StatusEmote square_silent => new StatusEmote("(square_silent)");

	public static StatusEmote square_drool => new StatusEmote("(square_drool)");

	public static StatusEmote square_sadbaby => new StatusEmote("(square_sadbaby)");

	public static StatusEmote square_cool => new StatusEmote("(square_cool)");

	public static StatusEmote square_kiss => new StatusEmote("(square_kiss)");

	public static StatusEmote square_happy => new StatusEmote("(square_happy)");

	public static StatusEmote square_surprised => new StatusEmote("(square_surprised)");

	public static StatusEmote square_tired => new StatusEmote("(square_tired)");

	public static StatusEmote square_yawn => new StatusEmote("(square_yawn)");

	public static StatusEmote square_hand => new StatusEmote("(square_hand)");

	public static StatusEmote square_angel => new StatusEmote("(square_angel)");

	public static StatusEmote square_zzz => new StatusEmote("(square_zzz)");

	internal StatusEmote(string emote)
	{
		Emote = emote;
	}
}
