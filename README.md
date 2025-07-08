# ColorTag

![SCP:SL Plugin](https://img.shields.io/badge/SCP--SL%20Plugin-blue?style=for-the-badge)
![Language](https://img.shields.io/badge/Language-C%23-blueviolet?style=for-the-badge)
![Downloads](https://img.shields.io/github/downloads/angelseraphim/ColorTag/total?label=Downloads&color=333333&style=for-the-badge)

---
ColorTag is a plugin for SCP: Secret Laboratory using LabApi, plugin that allows players to create their own individual rainbow tag.
---

## ⚙️ Configuration (`config.yml`)

```yaml
data_path: '%config%/%data%'
# Rights so that the player can change his colors
color_require_permission: ''
# Default limit (If the player's group is not in GroupColorLimit)
default_color_limit: 5
# Limit for specific groups
group_color_limit: []
# Rights to remove colors from a player
admin_require_permission: ''
# Rights to delete data base
drop_data_require_permission: ''
# Interval for color change
interval: 1
# Translations
translation:
  dont_have_permissions: ''
  not_found_in_data_base: ''
  other_not_found: ''
  color_limit: ''
  invalid_color: ''
  kill_data_base: ''
  successfull: ''
  successfull_deleted: ''
```

### Parameter Descriptions:

* `data_path` — Path to the database.
* `color_require_permission` — Right to change color.
* `default_color_limit` — Default limit (Limit for unspecified groups).
* `group_color_limit` — Limits per group.
* `admin_require_permission` — Rights to remove colors from players.
* `drop_data_require_permission` — Right to clear the database.
* `interval` — Color update time.
* `translation` — Text of the response from the plugin.
